using System.Collections;
using UnityEngine;

public class PlayerEarthAttacks : MonoBehaviour
{
    private CharacterMover characterMover; 
    public GameObject earthWall;
    public bool attackStatus = false;
    public bool moveAttackStatus = true;
    public bool isGrounded;
    private bool canAttack = true; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        characterMover = GetComponent<CharacterMover>();
    }

    private void FixedUpdate()
    {
        groundedCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded && !IsAttacking() && canAttack)
        {
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


            canAttack = false;

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

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(10f);
        canAttack = true;
    }

    IEnumerator PerformEarthAttack(Vector3 offsetPosition, bool attackDirection)
    {
        moveAttackStatus = true; 
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

        moveAttackStatus = false;
    }

    public bool IsAttacking()
    {
        return moveAttackStatus;
    }
}
