using UnityEngine;
using UnityEngine.Rendering;

public class CharacterIMG : MonoBehaviour
{
    public Animator animator;
    private PlayerFireAttacks playerFireAttacks;
    private PlayerAirAttacks playerAirAttacks;
    private CharacterMover characterMover;
    private playerHealthScript playerHealthScript;

    private bool prevMoveAttackStatus;
    private bool prevAirAttackStatus; // New variable to track previous air attack status
    private bool isJumpingTriggered;
    private bool wasGrounded;

    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
        playerHealthScript = GetComponent<playerHealthScript>();
        playerFireAttacks = GetComponent<PlayerFireAttacks>();
        playerAirAttacks = GetComponent<PlayerAirAttacks>();
        prevMoveAttackStatus = false;
        prevAirAttackStatus = false; // Initialize the new variable
        isJumpingTriggered = false;
        wasGrounded = true;
    }

    void Update()
    {
        RotateAnimator();
        animator.SetBool("Running", characterMover.Running);

        // Fire attack logic
        if (playerFireAttacks.moveAttackStatus && !prevMoveAttackStatus)
        {
            animator.SetTrigger("fireAttack");
        }

        // Air attack logic (ensuring animation is triggered only once per attack)
        if (playerAirAttacks.moveAttackStatus && !prevAirAttackStatus)
        {
            Debug.Log("Setting airAttack trigger");
            animator.SetTrigger("airAttack");
        }

        // Update previous attack statuses
        prevMoveAttackStatus = playerFireAttacks.moveAttackStatus;
        prevAirAttackStatus = playerAirAttacks.moveAttackStatus;

        // Hit logic
        animator.SetBool("isHit", playerHealthScript.stunned);

        // Jumping logic
        if (!characterMover.isGrounded && wasGrounded)
        {
            animator.SetTrigger("Jumping");
            isJumpingTriggered = true;
        }

        bool isIdle = !characterMover.Running &&
                      !playerAirAttacks.moveAttackStatus &&
                      !playerHealthScript.stunned &&
                      !playerFireAttacks.moveAttackStatus &&
                      !animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping") &&
                      !animator.GetCurrentAnimatorStateInfo(0).IsName("fireAttack");

        animator.SetBool("Idle", isIdle);
        wasGrounded = characterMover.isGrounded;
    }

    void RotateAnimator()
    {
        if (!characterMover.characterRotate)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
