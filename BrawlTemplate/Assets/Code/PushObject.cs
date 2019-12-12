using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    Rigidbody rig;
    public float moveSpeed = 5f;
    public bool canPush;
    public Transform player;
    public Vector3 moveDirection;
    public Animator anim;
    public enum Direction
    {
        Forward,
        Backwards,
        Left,
        Right
    }
    public Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = player.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TestDirection();
        if (canPush == true)
        {
            MoveObject();
        }
    }



    public void TestDirection()
    {

        Vector3 VectorResult;
        float DotResult = Vector3.Dot(transform.forward, player.forward);
        if (DotResult > 0)
        {
            VectorResult = transform.forward + player.forward;
        }
        else
        {
            VectorResult = transform.forward - player.forward;
        }
        Debug.DrawRay(transform.position, VectorResult * 100, Color.green);
    }

    public void MoveObject()
    {
        // rig.AddForce(Vector3.right * moveSpeed * Time.deltaTime);
        // rig.velocity = new Vector3(1*moveSpeed * Time.deltaTime,0,0);
        if (direction == Direction.Forward)
        {
            moveDirection = Vector3.forward;
        }
        if (direction == Direction.Backwards)
        {
            moveDirection = -Vector3.forward;
        }
        if (direction == Direction.Left)
        {
            moveDirection = Vector3.left;
        }
        if (direction == Direction.Right)
        {
            moveDirection = Vector3.right;
        }

        rig.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            canPush = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {


            canPush = false;
        }
    }
}
