using UnityEngine;

public class CharacterIMG : MonoBehaviour
{
    public Animator animator;
    private PlayerFireAttacks playerFireAttacks;
    private CharacterMover characterMover;
    private playerHealthScript playerHealthScript;

    private bool prevMoveAttackStatus; // Keep track of previous moveAttackStatus
    private bool isJumpingTriggered; // Flag to track if jumping animation is triggered
    private bool wasGrounded; // Track grounded state from previous frame

    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
        playerHealthScript = GetComponent<playerHealthScript>();
        playerFireAttacks = GetComponent<PlayerFireAttacks>();
        prevMoveAttackStatus = false; // Initialize prevMoveAttackStatus
        isJumpingTriggered = false; // Initialize jumping animation trigger flag
        wasGrounded = true; // Assuming character starts grounded
    }

    void Update()
    {
        // Fire attack logic
        if (playerFireAttacks.moveAttackStatus && !prevMoveAttackStatus && !characterMover.characterRotate)
        {
            animator.SetTrigger("fireAttack");
        }

        // Hit logic
        if (playerHealthScript.stunned)
        {
            animator.SetBool("isHit", true);
        }
        else
        {
            animator.SetBool("isHit", false);
        }

        // Jumping logic
        if (!characterMover.isGrounded && wasGrounded)
        {
            animator.SetTrigger("Jumping");
            isJumpingTriggered = true; // Set the flag to true to indicate jumping animation is triggered
        }

        // Set idle if all conditions are false
        if (characterMover.isGrounded && !playerHealthScript.stunned && !playerFireAttacks.moveAttackStatus && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping") && !animator.GetCurrentAnimatorStateInfo(0).IsName("fireAttack"))
        {
            animator.SetTrigger("Idle");
        }

        // Update states for the next frame
        prevMoveAttackStatus = playerFireAttacks.moveAttackStatus;
        wasGrounded = characterMover.isGrounded;
    }
}
