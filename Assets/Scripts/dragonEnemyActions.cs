using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class dragonEnemyActions : MonoBehaviour
{
    public Animator animator;
    private dragonEnemyScript DragonEnemyScript;
    public GameObject bullet;
    public GameObject player;
    private System.Random rand = new System.Random();
    private List<string> attacks = new List<string> { "flyBy", "orderRocks", "randRocks", "Fire", "Wait" };
    private bool dragonRotation = true;

    private void Start()
    {
        DragonEnemyScript = GetComponent<dragonEnemyScript>();
        chooseAttack();
    }



    void chooseAttack()
    {
        string selectedAttack = attacks[rand.Next(attacks.Count)];
        Debug.Log("Enemy performs " + selectedAttack);
        if (selectedAttack == "flyBy")
        {
            flyBy();
        }
        if (selectedAttack == "randRocks")
        {
            randRocks();
        }
        if (selectedAttack == "Fire")
        {
            Fire();
        }
        if (selectedAttack == "orderRocks")
    {
        orderRocks();
        if (selectedAttack == "Wait")
        {
            waitAttackCoroutine();
        }
    }
    }

    void orderRocks()
    {
    }
    void randRocks()
    {
    }

    void flyBy()
    {
    }
    void Fire()
    {
    }

    IEnumerator waitAttackCoroutine()
    {
        yield return new WaitForSeconds(3);
        chooseAttack();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 3)
        {
            playerHealthScript PlayerHealthScript = collision.gameObject.GetComponent<playerHealthScript>();
            PlayerHealthScript.hit();
            Debug.Log("Player hit");

        }
        if (collision.gameObject.layer == 7)
        {
            Destroy(collision.gameObject);
        }
    }
}
