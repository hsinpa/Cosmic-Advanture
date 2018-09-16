using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseUnit : MonoBehaviour {

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

    Rigidbody rb;

    private Vector3 curPosition;
    private MoveDir nextDir;
    private bool isHolding;
    private bool isLanding;
    private float initialYPos;

    private float moveTimeStamp = 0;
    private float moveTimePeroid = 0.1f;

    public void SetUp()
    {
        rb = GetComponent<Rigidbody>();
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
        isHolding = true;
    }

	public virtual bool Move(MoveDir p_direction) {

        //Currently moving
        isHolding = false;


        if (!isLanding || Time.time < moveTimeStamp ||
            (p_direction.direction == Vector3.zero && !p_direction.enable))
            return false;

        nextDir = p_direction;
        if (!p_direction.enable) {
            // transform.localScale = Vector3.one;
            return false;
        }

        rb.AddForce(0, jumpForce, 0);
        isLanding = false;
        moveTimeStamp = Time.time + moveTimePeroid;

        return true;
    }

    public struct MoveDir {
        public Vector3 direction;
        public bool enable;

        public MoveDir(Vector3 direction, bool enable)
        {
            this.direction = direction;
            this.enable = enable;
        }
    }

}
