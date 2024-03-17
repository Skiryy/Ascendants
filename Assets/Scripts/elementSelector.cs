using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class elementSelector : MonoBehaviour
{
    public GameObject element;
    private PlayerFireAttacks playerFireAttacks;
    private PlayerEarthAttacks playerEarthAttacks;
    private PlayerWaterAttacks playerWaterAttacks;
    private PlayerAirAttacks playerAirAttacks;
    private CharacterMover characterMover;
    public Button fireButton;
    public Button earthButton;

    void Start()
    {
        playerFireAttacks = GetComponent<PlayerFireAttacks>();
        playerEarthAttacks = GetComponent<PlayerEarthAttacks>();
        playerWaterAttacks = GetComponent<PlayerWaterAttacks>(); 
        playerAirAttacks = GetComponent<PlayerAirAttacks>();
        characterMover = GetComponent<CharacterMover>();
        playerFireAttacks.enabled = false;
        playerEarthAttacks.enabled = false;
        playerWaterAttacks.enabled = false;

        // Check if the fireButton is assigned
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.visible = true;
            element.SetActive(true);
            characterMover.enabled = false;
        }
        else
        {
            element.SetActive(false);
            characterMover.enabled = true;
            Cursor.visible = false;
        }
    }

    public void fireSelected()
    {
        Debug.Log("Selected Fire");
        playerFireAttacks.enabled = true;
        playerEarthAttacks.enabled = false;
        playerWaterAttacks.enabled = false;
        playerAirAttacks.enabled = false;
        characterMover.jumpForce = 7f;
    }

    public void waterSelected()
    {
        Debug.Log("Selected Water");
        playerWaterAttacks.enabled = true;
        playerEarthAttacks.enabled = false;
        playerFireAttacks.enabled = false;
        playerAirAttacks.enabled = false;
        characterMover.jumpForce = 7f;
        // Implement waterSelected logic if needed
    }

    public void airSelected()
    {
        // Implement airSelected logic if needed
        Debug.Log("Selected Air");
        playerEarthAttacks.enabled = false;
        playerFireAttacks.enabled = false;
        playerWaterAttacks.enabled = false;
        playerAirAttacks.enabled = true;
        characterMover.jumpForce = 12f;
    }

    public void earthSelected()
    {
        // Implement earthSelected logic if needed
        Debug.Log("Selected Earth");
        playerEarthAttacks.enabled = true;
        playerFireAttacks.enabled = false;
        playerWaterAttacks.enabled = false;
        playerAirAttacks.enabled = false;
        characterMover.jumpForce = 7f;
    }
}
