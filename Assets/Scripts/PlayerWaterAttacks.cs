using System.Collections;
using UnityEngine;

public class PlayerWaterAttacks : MonoBehaviour
{
    private CharacterMover characterMover; // Reference to the CharacterMover script
    public GameObject icicle;
    public bool attackStatus = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        // Find and store the CharacterMover script component
        characterMover = GetComponent<CharacterMover>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for left mouse button click (button index 0) and if not currently attacking
        if (Input.GetMouseButtonDown(0) && !attackStatus)
        {
            // Access the characterRotate variable from CharacterMover script
            bool rotateValue = characterMover.characterRotate;
            // Execute attack action
            StartCoroutine(PerformAttack());        }
    }

    IEnumerator PerformAttack()
    {
        attackStatus = true;

        // Implement a delay of 2 seconds before allowing another attack
        bool attackDirection = characterMover.characterRotate;
        Vector3 playerPosition = transform.position;
        int direction = (attackDirection) ? 1 : -1;
        Vector3 offsetPosition = playerPosition + Vector3.right * 01f * direction;

        if (attackDirection == false)
        {
            GameObject icicleAttadckInstance = Instantiate(icicle, offsetPosition, Quaternion.Euler(0, 270, Random.Range(0f, 360f)));
            //FireAttackInstance.transform.Translate(Vector3.left * direction);
            Destroy(icicleAttadckInstance, 2f);
        }
        else
        {
            GameObject icicleAttadckInstance = Instantiate(icicle, offsetPosition, Quaternion.Euler(0, 90, Random.Range(0f, 360f)));
            //FireAttackInstance.transform.Translate(Vector3.left * direction);
            Destroy(icicleAttadckInstance, 2f);
        }
        yield return new WaitForSeconds(0.5f);
        attackStatus = false;
    }
}
