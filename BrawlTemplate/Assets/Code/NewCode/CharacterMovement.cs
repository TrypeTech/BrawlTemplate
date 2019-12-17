using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

   private CharacterController Controller;
    Animator anim;
    
    
    [Header("Movement Peramtiters")]

    public float JumpForce = 20f;
    float RotationAngle;

    // JumpVariables

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;


  
    public float jumpTime = 0.35f;
    public float DoubleJumpTime = 0.17f;
    private bool canJump;
    private bool canDoubleJump;
    public int MaxDoubleJumps = 1;
    private int doubleJumpCount;
    // Double jump stuff
    public int extraJumps;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }



    void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");


        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Horizontal, 0, 0);
            moveDirection *= speed;

            // begin jump
            if (Input.GetButtonDown("Jump") )
            {
                // start count down of jump
                StartCoroutine(Count(jumpTime));
                canJump = true;
                doubleJumpCount = MaxDoubleJumps;
            }
            Debug.Log(canJump);
        }

        // jump up contenually
        if (Input.GetButton("Jump") && canJump == true)
        {
            moveDirection.y = jumpSpeed;
            // do jump
        }

        // hella needed glitch if count down no more fall
         if(Input.GetButton("Jump") && canJump == false)
        {
            
            moveDirection.y -= gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


         if(Input.GetButtonUp("Jump") && !controller.isGrounded)
        {
            canDoubleJump = true;
        }

         if(Input.GetButtonDown("Jump") && !controller.isGrounded && canDoubleJump == true && doubleJumpCount > 0)
        {
            moveDirection.y = jumpSpeed;
            doubleJumpCount -= 1;
            StartCoroutine(Count(DoubleJumpTime)); 
        }


        if (controller.isGrounded)
        {
            moveDirection.y -= gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(!controller.isGrounded && !Input.GetButton("Jump"))
        {
            moveDirection.y -= gravity * (lowJumpMultiplier -1) *  Time.deltaTime;
        }
      
        controller.Move(moveDirection * Time.deltaTime);


        
        // Rotation 

        if (Horizontal > 0)
        {
            RotationAngle = 90f;
        }
        else if (Horizontal < 0)
        {
            RotationAngle = -90f;
        }

        Vector3 angle = new Vector3(0, RotationAngle, 0);
        transform.eulerAngles = angle;
    }

    IEnumerator Count(float time)
    {
        canJump = true;
        Debug.Log(canJump);
        yield return new WaitForSeconds(time);
        canJump = false;
        canDoubleJump = false;
        Debug.Log(canJump);
    }
}
