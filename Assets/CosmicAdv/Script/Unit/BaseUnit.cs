using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseUnit : MonoBehaviour {

    public GameObject modelPrefab;
    public BaseStat baseStat;

    public float jumpForce = 100;

    public float speed = 5;
    public float speedRot = 1000;

    //public float rotationOffset;

    Rigidbody rb;

    public Vector3 curPosition;
    private Vector3 nextDir;


    public Animator animator {
        get {
            return GetComponentInChildren<Animator>();
        }
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetPosition();
    }


    void Update()
    {

        if (nextDir != Vector3.zero)
        {

            float dist = Vector3.Distance(transform.position, curPosition + nextDir);
            if (dist < 0.1f)
            {
                ResetPosition();
            }
            else {
                transform.position = Vector3.MoveTowards(transform.position, curPosition + nextDir, speed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nextDir), speedRot * Time.deltaTime);
            }


        }

    }

    private void ResetPosition() {
        nextDir = Vector3.zero;
        curPosition = transform.position;
        curPosition.x = Mathf.Round(curPosition.x);
        curPosition.z = Mathf.Round(curPosition.z);
    }

    public virtual void OnClick() {

    }

	public virtual void Move(Vector3 p_direction) {
        rb.AddForce(0, jumpForce, 0);
        nextDir = p_direction;
    }

}
