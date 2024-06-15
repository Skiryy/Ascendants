using System.Collections;
using UnityEngine;

public class PlayerEarthAttacks : MonoBehaviour
{
    private CharacterMover characterMover; // Reference to the CharacterMover script
    public GameObject earthWall;
    public bool attackStatus = false;
    public bool moveAttackStatus = true;
    public bool isGrounded;
    private bool canAttack = true; // Flag to control attack cooldown

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        // Find and store the CharacterMover script component
        characterMover = GetComponent<CharacterMover>();
    }

    private void FixedUpdate()
    {
        groundedCheck();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for left mouse button click (button index 0), if not currently attacking, and if the attack cooldown has passed
        if (Input.GetMouseButtonDown(0) && isGrounded && !IsAttacking() && canAttack)
        {
            // Access the characterRotate variable from CharacterMover script
            bool attackDirection = characterMover.characterRotate;
            Vector3 playerPosition = transform.position;
            if (attackDirection)
            {
                Vector3 offsetPosition = playerPosition + Vector3.right * 0.5f;
                StartCoroutine(PerformEarthAttack(offsetPosition, attackDirection));

            }
            else if (!attackDirection)
            {
                Vector3 offsetPosition = playerPosition + Vector3.left * 0.5f;
                StartCoroutine(PerformEarthAttack(offsetPosition, attackDirection));

            }

            // Spawn the earthWall

            // Set the canAttack flag to false, initiating the cooldown
            canAttack = false;

            // Use Invoke to set canAttack to true after 10 seconds
            StartCoroutine(ResetAttackCooldown());
        }
    }

    void groundedCheck()
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

    // Method to reset the canAttack flag after the cooldown period
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(10f);
        canAttack = true;
    }

    // Coroutine for the Earth Attack
    IEnumerator PerformEarthAttack(Vector3 offsetPosition, bool attackDirection)
    {
        moveAttackStatus = true; // Set moveAttackStatus to true when attacking
        if (attackDirection) { 
        GameObject earthWallInstance = Instantiate(earthWall, offsetPosition, Quaternion.Euler(90, 0, 180));
        Destroy(earthWallInstance, 20f);
        }
        else if (!attackDirection)
        {
            GameObject earthWallInstance = Instantiate(earthWall, offsetPosition, Quaternion.Euler(90, 0, 0));
            Destroy(earthWallInstance, 20f);
        }

        yield return new WaitForSeconds(0.5f);
        //earth bending animation

        // Set the moveAttackStatus to false after the delay
        moveAttackStatus = false;
    }

    // Method to check if the player is currently attacking
    public bool IsAttacking()
    {
        return moveAttackStatus;
    }
}
