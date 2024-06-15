using System.Collections;
using UnityEngine;

public class PlayerAirAttacks : MonoBehaviour
{
    private CharacterMover characterMover; // Reference to the CharacterMover script
    public GameObject airAttack;
    public bool attackStatus = false;
    public bool animationstatus;
    public bool moveAttackStatus = false;

    void Start()
    {
        Cursor.visible = false;
        characterMover = GetComponent<CharacterMover>();
    }

    void Update()
    {
        // Check for left mouse button being held down (button index 0) and if not currently attacking
        if (Input.GetMouseButton(0) && !attackStatus)
        {
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
        Debug.Log("animation status true");
        // Perform the attack action
        bool attackDirection = characterMover.characterRotate;
        Vector3 playerPosition = transform.position;
        int direction = (attackDirection) ? 1 : -1;
        Vector3 offsetPosition = playerPosition + Vector3.right * 0.1f * direction;

        if (!attackDirection)
        {
            GameObject airAttackInstance = Instantiate(airAttack, offsetPosition, Quaternion.Euler(0, 270, 0));
            Destroy(airAttackInstance, 1f);
        }
        else
        {
            GameObject airAttackInstance = Instantiate(airAttack, offsetPosition, Quaternion.Euler(0, 90, 0));
            Destroy(airAttackInstance, 1f);
        }
        // Wait for 0.5 seconds before allowing the next attack
        yield return new WaitForSeconds(0.5f);
        moveAttackStatus = false;
        attackStatus = false;
    }
}
