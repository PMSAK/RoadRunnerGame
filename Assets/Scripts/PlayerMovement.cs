using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class PlayerMovement : MonoBehaviour
{
    //Ground Moveemnt
    [SerializeField] float speed;
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float sprintSpeed = 20f; 

    [SerializeField] float rotationSpeed = 100f;

    //Jumping
    [SerializeField] float yJump = 10f;
    [SerializeField] float groundCheckDistance = 1.2f;
    bool IsGrounded = true;
    [SerializeField] float walkairMultiplier = 0.5f;
    [SerializeField] float sprintAirMultiplier = 0.8f;

    bool canDoubleJump;

    [SerializeField] float additionalGravity = 5f;
    

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        GroundCheck();

        SpeedChange();
        Jump();
        AdditionGravity();
    }

    void FixedUpdate()
    {

        MovePlayer();   
    }

    void MovePlayer()
    {
        float currentSpeed = speed;
        if (!IsGrounded && currentSpeed == walkSpeed)
        {
            currentSpeed*=walkairMultiplier;
        }

        else if (!IsGrounded && currentSpeed == sprintSpeed)
        {
            currentSpeed*=sprintAirMultiplier;
        }

        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));
        
        float verticalMovement = Input.GetAxis("Vertical") * currentSpeed;

        Vector3 moveDirection = transform.forward * verticalMovement;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }

    void SpeedChange()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }

        else
        {
            speed = walkSpeed;
        }
    }

    void GroundCheck()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Vector3 JumpForce = new Vector3(0f, yJump, 0f);
            rb.AddForce(JumpForce, ForceMode.Impulse);
            canDoubleJump = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            Vector3 JumpForce = new Vector3(0f, yJump, 0f);
            rb.AddForce(JumpForce, ForceMode.Impulse);
            canDoubleJump = false;
        }
        
    }

    void AdditionGravity()
    {
        if (!IsGrounded)
        {
            rb.AddForce(Vector3.down * additionalGravity, ForceMode.Force);
        }
    }
}
