using System.Collections;
using UnityEngine;

public class PlayerAirAttacks : MonoBehaviour
{
    private CharacterMover characterMover; // Reference to the CharacterMover script
    public bool attackStatus = false;
    public bool moveAttackStatus = false;
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
        if (Input.GetMouseButtonDown(0))
        {
            // Access the characterRotate variable from CharacterMover script
            bool rotateValue = characterMover.characterRotate;
        }
    }
}
