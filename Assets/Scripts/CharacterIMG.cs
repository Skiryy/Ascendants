using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIMG : MonoBehaviour
{

    public GameObject StandingStill;
    public GameObject shootingFire;
    public GameObject shootingWater;
    private PlayerFireAttacks playerFireAttacks;
    private PlayerEarthAttacks playerEarthAttacks;
    private PlayerWaterAttacks playerWaterAttacks;
    private PlayerAirAttacks playerAirAttacks;

    // Start is called before the first frame update
    void Start()
    {
        StandingStill.SetActive(true);  
        playerFireAttacks = GetComponent<PlayerFireAttacks>();
        playerEarthAttacks = GetComponent<PlayerEarthAttacks>();
        playerWaterAttacks = GetComponent<PlayerWaterAttacks>();
        playerAirAttacks = GetComponent<PlayerAirAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFireAttacks.moveAttackStatus == true) {
            StandingStill.SetActive(false);
            shootingFire.SetActive(true);
            shootingWater.SetActive(false);

        }
        else if (playerWaterAttacks.attackStatus == true)
        {
            shootingWater.SetActive(true);
            StandingStill.SetActive(false);
            shootingFire.SetActive(false);
        }
        else
        {
            StandingStill.SetActive(true);
            shootingFire.SetActive(false);
            shootingWater.SetActive(false);
        }
    }
}
