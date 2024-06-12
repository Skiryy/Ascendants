using UnityEngine;

public class CharacterIMG : MonoBehaviour
{
    public Animator animator;
    private PlayerFireAttacks playerFireAttacks;
    private CharacterMover characterMover;
    private playerHealthScript playerHealthScript;

    private bool prevMoveAttackStatus; 
    private bool isJumpingTriggered; 
    private bool wasGrounded; 

    void Start()
    {
        characterMover = GetComponent<CharacterMover>();
        playerHealthScript = GetComponent<playerHealthScript>();
        playerFireAttacks = GetComponent<PlayerFireAttacks>();
        prevMoveAttackStatus = false; 
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

        // Hit logic
        animator.SetBool("isHit", playerHealthScript.stunned);

        // Jumping logic
        if (!characterMover.isGrounded && wasGrounded)
        {
            animator.SetTrigger("Jumping");
            isJumpingTriggered = true; 
        }

        bool isIdle = !characterMover.Running &&
                      !playerHealthScript.stunned &&
                      !playerFireAttacks.moveAttackStatus &&
                      !animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping") &&
                      !animator.GetCurrentAnimatorStateInfo(0).IsName("fireAttack");

        animator.SetBool("Idle", isIdle);
        prevMoveAttackStatus = playerFireAttacks.moveAttackStatus;
        wasGrounded = characterMover.isGrounded;
    }

    void RotateAnimator()
    {
        if (!characterMover.characterRotate) { 
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
