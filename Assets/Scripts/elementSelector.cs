using System.Collections;
using System.Collections.Generic;   
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
        playerAirAttacks.enabled = false;

    }

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
        if (Input.GetKey(KeyCode.Alpha1)){
            fireSelected();
        }
        if (Input.GetKey(KeyCode.Alpha2)){
            waterSelected();
        }
        if (Input.GetKey(KeyCode.Alpha3)){
            airSelected();
        }
        if (Input.GetKey(KeyCode.Alpha4))   {
            earthSelected();
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
    }

    public void airSelected()
    {
        Debug.Log("Selected Air");
        playerEarthAttacks.enabled = false;
        playerFireAttacks.enabled = false;
        playerWaterAttacks.enabled = false;
        playerAirAttacks.enabled = true;
        characterMover.jumpForce = 12f;
    }

    public void earthSelected()
    {
        Debug.Log("Selected Earth");
        playerEarthAttacks.enabled = true;
        playerFireAttacks.enabled = false;
        playerWaterAttacks.enabled = false;
        playerAirAttacks.enabled = false;
        characterMover.jumpForce = 7f;
    }
}
