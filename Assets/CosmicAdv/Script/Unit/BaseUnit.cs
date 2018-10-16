using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseUnit : MapComponent {

    public BaseStat baseStat;

    public float jumpForce = 100;

    public float speed = 5;
    public float speedRot = 1000;
    private float shrinkValue = 0.4f;
    private float shrinkOffset {
        get {
           return 1 - shrinkValue;
        }
    }

    //public float rotationOffset;
    private Rigidbody rb;
    private Vector3 curPosition;
    private MoveDir nextDir;
    private bool isHolding;
    private bool isLanding;
    private float initialYPos;

    private float moveTimeStamp = 0;
    [SerializeField]
    private float moveTimePeroid = 0.1f;

    [SerializeField]
    private float attackTimePeroid = 0.5f;

    public void SetUp()
    {
        baseStat = GetComponent<BaseStat>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        ResetPosition();

        initialYPos = transform.position.y;
    }

    void Update()
    {
        if (nextDir.direction != Vector3.zero) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nextDir.direction), speedRot * Time.deltaTime);
            float lerpYScale = Mathf.Lerp(transform.localScale.y, 1, 0.4f);
            transform.localScale = new Vector3(1, lerpYScale, 1);
        }

        if (nextDir.enable)
        {
            Vector3 unitPos = new Vector3(transform.position.x, 0, transform.position.z),
                    targetPos = curPosition + nextDir.direction;
                    targetPos.y = unitPos.y;
            float dist = Vector3.Distance(unitPos, targetPos);

            if (dist < 0.001f)
            {
                //Debug.Log("UnitPos" + unitPos +", TargetPos" + targetPos);
                ResetPosition();
            }
            else {
                transform.position = Vector3.MoveTowards(transform.position, curPosition + nextDir.direction, speed * Time.deltaTime);
            }
        } else if (isHolding) {
            //Scale models
            float lerpYScale = Mathf.Lerp(transform.localScale.y, shrinkValue, 0.3f);

            transform.localScale = new Vector3(1, lerpYScale, 1);
        }
    }

    private void ResetPosition() {
        nextDir.enable = false;
        nextDir.direction = Vector3.zero;
        curPosition = transform.position;
        curPosition.x = Mathf.Round(curPosition.x);
        curPosition.z = Mathf.Round(curPosition.z);
        transform.position = curPosition;
        transform.localScale = Vector3.one;
        isLanding = true;
    }

    public virtual void OnClick() {
        if (Time.time > moveTimeStamp) isHolding = true;
    }

    public void ProcessMoveAction(CA_Terrain.CA_Grid p_next_grid, Vector3 p_direction, System.Action<bool> p_callback) {
        MoveDir moveDir = new MoveDir(p_direction, p_next_grid.isWalkable);
        MoveState isMove = Move(moveDir);

        //Check if mapcomponent is baseunit, if yes and in different team, then try to attack it
        if (isMove.state == MoveState.State.Fail_PosInvalid && p_next_grid.position != Vector2.zero)
        {
            if (p_next_grid.mapComponent.GetType() == typeof(BaseUnit)) {
                BaseUnit targetUnit = (BaseUnit)p_next_grid.mapComponent;
                Attack(targetUnit);
            }
        }

        p_callback?.Invoke(isMove.isSuccessful);
    }

    private MoveState Move(MoveDir p_direction) {
        //Currently moving
        isHolding = false;

        if (!isLanding || Time.time < moveTimeStamp ||
            (p_direction.direction == Vector3.zero && !p_direction.enable))
            return new MoveState(false, MoveState.State.Fail_NotReady);

        nextDir = p_direction;
        if (!p_direction.enable) {
            return new MoveState(false, MoveState.State.Fail_PosInvalid);
        }

        rb.AddForce(0, jumpForce, 0);
        isLanding = false;
        moveTimeStamp = Time.time + moveTimePeroid;

        return new MoveState(true, MoveState.State.Success);
    }

    private void Attack(BaseUnit p_target) {
        if (p_target.baseStat.team_label != baseStat.team_label)
        {
            moveTimeStamp = Time.time + attackTimePeroid;
            MainApp.Instance.subject.notify(EventFlag.Game.UnitAttack, this, p_target);   
        }
    }

    public void OnAttack(BaseUnit p_attacker) {
        baseStat.hp -= p_attacker.baseStat.attack;
        if (baseStat.hp <= 0) {
            MainApp.Instance.subject.notify(EventFlag.Game.UnitDestroy, this);
        }
    }

    private struct MoveState {
        public bool isSuccessful;
        public State state;


        public MoveState(bool successful, State state)
        {
            this.isSuccessful = successful;
            this.state = state;
        }

        public enum State {
            Success,
            Fail_NotReady,
            Fail_PosInvalid
        }
    }

    private struct MoveDir {
        public Vector3 direction;
        public bool enable;

        public MoveDir(Vector3 direction, bool enable)
        {
            this.direction = direction;
            this.enable = enable;
        }
    }
}
