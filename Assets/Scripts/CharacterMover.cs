using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour
{
    private PlayerFireAttacks playerFireAttacks;
    private PlayerEarthAttacks playerEarthAttacks;
    public float moveSpeed = 5f;
    public float airMoveSpeed = 2.5f;
    public float jumpForce = 10f;
    public float crouchScale = 0.5f;
    public float downForce = 8f;
    public bool characterRotate;
    public bool jumping;

    public bool isGrounded;
    private bool isCrouching;
    public bool Running; // Add this line

    void Start()
    {
        playerFireAttacks = GetComponent<PlayerFireAttacks>();
        playerEarthAttacks = GetComponent<PlayerEarthAttacks>();
    }

    void Update()
    {

        HandleInput();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Crouch();
    }

    void HandleInput()
    {
        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, jumpForce, 0f);
        }

        // Crouching
        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            isCrouching = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, -downForce, 0f);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            characterRotate = false;
            Debug.Log(characterRotate);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            characterRotate = true;
            Debug.Log(characterRotate);
        }
    }

    void Move()
    {
        // Check if player is attacking, set speed to 0 if true
        if (playerFireAttacks.IsAttacking() || playerEarthAttacks.IsAttacking())
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Running = false; // Add this line
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, GetComponent<Rigidbody>().velocity.y, 0f);
        Vector3 airMovement = new Vector3(horizontalInput * airMoveSpeed, GetComponent<Rigidbody>().velocity.y, 0f);

        if (isGrounded)
        {
            GetComponent<Rigidbody>().velocity = movement;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = airMovement;
        }

        // Check horizontal velocity and set Running
        Running = Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > 0.1f; // Add this line
    }

    void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Crouch()
    {
        if (isCrouching)
        {
            transform.localScale = new Vector3(1f, crouchScale, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
