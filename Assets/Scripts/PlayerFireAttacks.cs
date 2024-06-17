using System.Collections;
using UnityEngine;

public class PlayerFireAttacks : MonoBehaviour
{
    private CharacterMover characterMover;
    public GameObject fireAttack;
    public bool attackStatus = false;
    public bool moveAttackStatus = false;

    void Start()
    {
        Cursor.visible = false;
        characterMover = GetComponent<CharacterMover>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attackStatus)
        {
            bool rotateValue = characterMover.characterRotate;
            StartCoroutine(PerformAttack());
            IsAttacking();
        }
    }

    public bool IsAttacking()
    {
        return moveAttackStatus;
    }

    IEnumerator PerformAttack()
    {
        attackStatus = true;
        moveAttackStatus = true;

        bool attackDirection = characterMover.characterRotate;
        Vector3 playerPosition = transform.position;
        int direction = (attackDirection) ? 1 : -1;
        Vector3 offsetPosition = playerPosition + Vector3.right * 0.1f * direction;

        if (attackDirection == false)
        {
            GameObject FireAttackInstance = Instantiate(fireAttack, offsetPosition, Quaternion.Euler(0, 270, 0));
            Destroy(FireAttackInstance, 1f);
        }
        else
        {
            GameObject FireAttackInstance = Instantiate(fireAttack, offsetPosition, Quaternion.Euler(0, 90, 0));
            Destroy(FireAttackInstance, 1f);
        }
        yield return new WaitForSeconds(0.5f);
        moveAttackStatus = false;
        yield return new WaitForSeconds(1f);
        attackStatus = false;
    }
}
