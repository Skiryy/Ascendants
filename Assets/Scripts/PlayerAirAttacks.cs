using System;
using System.Collections;
using UnityEngine;

public class PlayerAirAttacks : MonoBehaviour
{
    private CharacterMover characterMover; // Reference to the CharacterMover script
    public bool attackStatus = false;
    public float distanceToPlayer;
    public GameObject enemy;
    public GameObject handsL;
    public GameObject handsR;
    public Rigidbody rb;
    private bool coroutineStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        // Find and store the CharacterMover script component
        characterMover = GetComponent<CharacterMover>();
        rb = GetComponent<Rigidbody>();
        handsL.SetActive(false);
        handsR.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(rb.position, enemy.transform.position);
        bool rotateValue = characterMover.characterRotate;
        // Check for left mouse button click (button index 0) and if not currently attacking
        // Access the characterRotate variable from CharacterMover script
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(distanceToPlayer);
                if (rotateValue == false)
            {
                handsL.SetActive(true);
                StartCoroutine(DeactivateHandsAfterDelay(handsL));
                if (distanceToPlayer < 5)
                {
                    Debug.Log("Attack");    
                    enemyScript EnemyScript = enemy.GetComponent<enemyScript>();
                    EnemyScript.health -= 1f;
                }
            }
            else if (rotateValue == true)
            {
                {
                    handsR.SetActive(true);
                    StartCoroutine(DeactivateHandsAfterDelay(handsR));
                    if (distanceToPlayer < 5)
                    {
                        Debug.Log("Attack");
                        enemyScript EnemyScript = enemy.GetComponent<enemyScript>();
                        EnemyScript.health -= 1f;
                    }
                }
            }
        }

        IEnumerator DeactivateHandsAfterDelay(GameObject hands)
        {
            if (!coroutineStarted)
            {
                coroutineStarted = true;
                yield return new WaitForSeconds(0.5f);
                hands.SetActive(false);
                coroutineStarted = false;
            }
        }
    }
}