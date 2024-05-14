using UnityEngine;

public class CharacterIMG : MonoBehaviour
{
    public Animator animator;
    private PlayerFireAttacks playerFireAttacks;
    private CharacterMover characterMover;

    private bool prevMoveAttackStatus; // Keep track of previous moveAttackStatus
    private bool isJumpingTriggered; // Flag to track if jumping animation is triggered

    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
        playerFireAttacks = GetComponent<PlayerFireAttacks>();
        prevMoveAttackStatus = false; // Initialize prevMoveAttackStatus
        isJumpingTriggered = false; // Initialize jumping animation trigger flag
    }

    void Update()
    {
        // Check if moveAttackStatus has just become true in the current frame
        if (playerFireAttacks.moveAttackStatus && !prevMoveAttackStatus && !characterMover.characterRotate)
        {
            animator.SetTrigger("fireAttack");
        }
        else if (playerFireAttacks.moveAttackStatus && !prevMoveAttackStatus && characterMover.characterRotate)
        {
            // Fire animation right
        }
        else if (characterMover.isGrounded == false && !isJumpingTriggered)
        {
            animator.SetTrigger("Jumping");
            isJumpingTriggered = true; // Set the flag to true to indicate jumping animation is triggered
        }
        else if (characterMover.isGrounded)
        {
            isJumpingTriggered = false; // Reset the flag when character is grounded
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Idle");
        }


        // Update prevMoveAttackStatus for the next frame
        prevMoveAttackStatus = playerFireAttacks.moveAttackStatus;
    }
}
