using UnityEngine;

public class CharacterIMG : MonoBehaviour
{
    public Animator animator;
    private PlayerFireAttacks playerFireAttacks;
    private CharacterMover characterMover;

    private bool prevMoveAttackStatus; // Keep track of previous moveAttackStatus

    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
        playerFireAttacks = GetComponent<PlayerFireAttacks>();
        prevMoveAttackStatus = false; // Initialize prevMoveAttackStatus
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
        else
        {
            animator.SetTrigger("Idle");
        }

        // Update prevMoveAttackStatus for the next frame
        prevMoveAttackStatus = playerFireAttacks.moveAttackStatus;
    }
}
