using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonEnemyActions : MonoBehaviour
{
    public Animator animator;
    private dragonEnemyScript DragonEnemyScript;
    public GameObject rockPrefab; // Changed bullet to rockPrefab for clarity
    public GameObject player;
    private System.Random rand = new System.Random();
    private List<string> attacks = new List<string> { "randRocks", "Fire", "orderRocks", "Wait", "Lightning" };
    private bool dragonRotation = true;
    private Vector3 rightPosition = new Vector3(0.32f, 0.74f, 0f);
    private Vector3 leftPosition = new Vector3(-0.4f, 0.74f, 0f);
    public GameObject fireee;
    private Vector3 targetPosition;
    public GameObject lightningg;

    // Define positions where rocks will be instantiated
    public Vector3[] rockPositions = new Vector3[12];

    private void Start()
    {
        transform.position = rightPosition;
        DragonEnemyScript = GetComponent<dragonEnemyScript>();
        RotateAnimator(false); // Face right initially
        chooseAttack();
    }
    void chooseAttack()
    {
        string selectedAttack = attacks[rand.Next(attacks.Count)];
        Debug.Log("Enemy performs " + selectedAttack);
        if (selectedAttack == "randRocks")
        {
            StartCoroutine(randRocks());
        }
        if (selectedAttack == "Fire")
        {
            StartCoroutine(fire());
        }
        if (selectedAttack == "orderRocks")
        {
            StartCoroutine(orderRocks());
        }
        if (selectedAttack == "Wait")
        {
            StartCoroutine(waitAttackCoroutine());
        }
        if (selectedAttack == "Lightning")
        {
            StartCoroutine(lightning());
        }
    }
    void chooseAttackLightning()
    {
        string selectedAttack = attacks[rand.Next(attacks.Count)];
        Debug.Log("Enemy performs " + selectedAttack);
        if (selectedAttack == "randRocks")
        {
            StartCoroutine(randRocks());
        }
        if (selectedAttack == "Fire")
        {
            StartCoroutine(fire());
        }
        if (selectedAttack == "orderRocks")
        {
            StartCoroutine(orderRocks());
        }
        if (selectedAttack == "Wait")
        {
            StartCoroutine(waitAttackCoroutine());
        }
        if (selectedAttack == "Lightning")
        {
            StartCoroutine(waitAttackCoroutine());
        }
    }
    void chooseOrderRockAttack()
    {
        string selectedAttack = attacks[rand.Next(attacks.Count)];
        Debug.Log("Enemy performs " + selectedAttack);
        if (selectedAttack == "randRocks")
        {
            StartCoroutine(randRocks());
        }
        if (selectedAttack == "Fire")
        {
            StartCoroutine(fire());
        }
        if (selectedAttack == "orderRocks")
        {
            StartCoroutine(lightning());
        }
        if (selectedAttack == "Wait")
        {
            StartCoroutine(waitAttackCoroutine());
        }
        if (selectedAttack == "Lightning")
        {
            StartCoroutine(lightning());
        }
    }
    IEnumerator lightning()
    {
        animator.SetBool("Lightning", true);
        animator.SetBool("wozardIdle", false);
        yield return new WaitForSeconds(2);
        int lightningCharges = rand.Next(1, 5);
        float currentCharges = 0f;
        int newAttack = rand.Next(0, 1);
        if (newAttack == 0)
        {
            animator.SetBool("Lightning", false);
            animator.SetBool("wozardIdle", true);
            chooseAttackLightning();
            while (currentCharges < lightningCharges)
            {
                yield return new WaitForSeconds(1f);
                targetPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                yield return new WaitForSeconds(1f);
                Debug.Log("Lightning boltts" + currentCharges);
                GameObject lightning12 = Instantiate(lightningg, targetPosition, Quaternion.identity);
                Destroy(lightning12, 1f);
                currentCharges += 1;
            }
        }
        else if (newAttack == 1)
        {
            while (currentCharges < lightningCharges)
            {
                yield return new WaitForSeconds(1f);
                targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                yield return new WaitForSeconds(1f);
                Debug.Log("Lightning boltts" + currentCharges);
                GameObject lightning12 = Instantiate(lightningg, targetPosition, Quaternion.identity);
                Destroy(lightning12, 1f);
                currentCharges += 1;
                animator.SetBool("Lightning", false);
                animator.SetBool("wozardIdle", true);
            }
            chooseAttack();
        }
        else
        {
            Debug.Log("Buggin not thuggin");
        }


    }
    IEnumerator fire()
    {
        animator.SetBool("wozardIdle", false);
        animator.SetTrigger("FireLoading");
        yield return new WaitForSeconds(2);
        animator.SetBool("fireActive", true);
        fireee.SetActive(true);
        yield return new WaitForSeconds(3);
        animator.SetBool("fireActive", false);
        animator.SetBool("wozardIdle", true);
        fireee.SetActive(false);
        chooseAttack();
        
    }
    IEnumerator randRocks()
    {
        int numRocks = rand.Next(10, 40); 

        for (int i = 0; i < numRocks; i++)
        {
            float randomX = (float)rand.Next(-12, 13);
            Vector3 rockPosition = new Vector3(randomX, 14f, 0f); 

            GameObject rock = Instantiate(rockPrefab, rockPosition, Quaternion.identity);
            Destroy(rock, 3f);

            yield return new WaitForSeconds(0.3f); 
        }
        chooseAttack();
    }

    IEnumerator orderRocks()
    {
        for (int i = 0; i < rockPositions.Length; i++)
        {
            GameObject rock = Instantiate(rockPrefab, rockPositions[i], Quaternion.identity);
            Destroy(rock, 3f);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(0.3f);
        transform.position = leftPosition;
        chooseOrderRockAttack();
        RotateAnimator(true); // Face left
        for (int i = rockPositions.Length - 1; i >= 0; i--)
        {
            GameObject rock = Instantiate(rockPrefab, rockPositions[i], Quaternion.identity);
            Destroy(rock, 3f);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1);
        transform.position = rightPosition;
        RotateAnimator(false); 
    }

    void RotateAnimator(bool faceLeft)
    {
        if (faceLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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
