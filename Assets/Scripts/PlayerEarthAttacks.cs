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
            int direction = (attackDirection) ? 1 : -1;
            Vector3 offsetPosition = playerPosition + Vector3.right * 2f * direction;

            // Spawn the earthWall
            StartCoroutine(PerformEarthAttack(offsetPosition, attackDirection));

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

        GameObject earthWallInstance = Instantiate(earthWall, offsetPosition, Quaternion.Euler(0, 0, 0));
        Destroy(earthWallInstance, 5f);

        // Wait for 10 seconds before setting moveAttackStatus to false
        yield return new WaitForSeconds(1f);

        // Set the moveAttackStatus to false after the delay
        moveAttackStatus = false;
    }

    // Method to check if the player is currently attacking
    public bool IsAttacking()
    {
        return moveAttackStatus;
    }
}
