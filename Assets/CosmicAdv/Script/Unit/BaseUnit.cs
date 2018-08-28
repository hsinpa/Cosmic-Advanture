using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseUnit : MonoBehaviour {

    public BaseStat baseStat;

    public float jumpForce = 100;

    public float speed = 5;
    public float speedRot = 1000;
    private float shrinkValue = 0.6f;
    private float shrinkOffset {
        get {
           return 1 - shrinkValue;
        }
    }

    //public float rotationOffset;

    Rigidbody rb;

    private Vector3 curPosition;
    private Vector3 nextDir;
    private bool isHolding;
    private bool isLanding;
    private float initialYPos;

    public Animator animator {
        get {
            return GetComponentInChildren<Animator>();
        }
    }

    public void SetUp()
    {
        rb = GetComponent<Rigidbody>();
        ResetPosition();

        initialYPos = transform.position.y;
    }


    void Update()
    {
        if (nextDir != Vector3.zero)
        {
            float dist = Vector3.Distance(transform.position, curPosition + nextDir);
            if (dist < 0.05f)
            {
                ResetPosition();
            }
            else {
                transform.position = Vector3.MoveTowards(transform.position, curPosition + nextDir, speed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nextDir), speedRot * Time.deltaTime);
                float lerpYScale = Mathf.Lerp( transform.localScale.y, 1, 0.5f);
                transform.localScale = new Vector3(1, lerpYScale, 1);
            }
        } else if (isHolding) {
            //Scale models
            float lerpYScale = Mathf.Lerp(transform.localScale.y, shrinkValue, 0.3f);

            transform.localScale = new Vector3(1, lerpYScale, 1);
        }

        if (transform.position.y - initialYPos < 0.1f && !isLanding)
            isLanding = true;
    }

    private void ResetPosition() {
        nextDir = Vector3.zero;
        curPosition = transform.position;
        curPosition.x = Mathf.Round(curPosition.x);
        curPosition.z = Mathf.Round(curPosition.z);
        transform.position = curPosition;
        transform.localScale = Vector3.one;
    }

    public virtual void OnClick() {
        isHolding = true;
    }

	public virtual void Move(Vector3 p_direction) {
        //Currently moving
        if (nextDir != Vector3.zero && isLanding) return;
        rb.AddForce(0, jumpForce, 0);
        nextDir = p_direction;
        isHolding = false;
        isLanding = false;
    }

}
