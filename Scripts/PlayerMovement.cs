using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    
    [Header("Movement")]
    public float moveSpeed= 5f;
    float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.4f;

    [Header("Sprinting")]
    [SerializeField] private Camera cam;
    [SerializeField] float walkSpeed = 8;
    [SerializeField] float sprintSpeed = 15;
    [SerializeField] float acceleration = 10;

    [Header("Sprinting Fov")]
    [SerializeField] private float fov;
    [SerializeField] private float sprintFov = 90f;
    [SerializeField] private float sprintFovTime = 5f;

    [Header("Jumping")]
    public float JumpFoce = 8f;


    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    
    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;


    float horizontalMovement;
    float vertialMovement;

    [Header("Ground Detection")]
    bool isGrounded;
    float groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;
    Rigidbody rb;
    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start() 
    {
       rb = GetComponent<Rigidbody>(); 
       rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput() 
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        vertialMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * vertialMovement+ orientation.right * horizontalMovement;

    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * JumpFoce, ForceMode.Impulse);
        }   
    }

    void ControlSpeed() 
    {
        //keep && isGrounded if return to walkspeed mid air
        if (Input.GetKey(sprintKey)) 
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFov, sprintFovTime * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, Time.deltaTime);
        }
    }


    void ControlDrag() 
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
            Physics.gravity = new Vector3(0, -9.81f * 2, 0); 
        }
    }
   
    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}

