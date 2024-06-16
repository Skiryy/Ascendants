using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalEnemyActions : MonoBehaviour
{
    public Animator animator;
    private finalEnemyScript FinalEnemyScript;
    public GameObject player;
    private List<string> phaseOneAttacks = new List<string> { "iceAttack", "fireAttack", "rainAttack" };
    private List<string> phaseTwoAttacks = new List<string> { "iceAttack", "fireAttack", "rainAttack", "rockAttack", "ringAttack" };

    private System.Random rand = new System.Random();
    private float attackCooldown = 10f;
    private float lastAttackTime = 0f;
    private bool enemyRotation = false;
    private Vector3 leftposition = new Vector3(-10.24f, 1.26f, 0f);
    private Vector3 rightposition = new Vector3(7.09f, 1.26f, 0f);
    private Vector3 leftpositionP2 = new Vector3(-13f, 1.26f, 0f);
    private Vector3 rightpositionP2 = new Vector3(12f, 1.26f, 0f);
    private Vector3 leftpositionP3 = new Vector3(-5.6f, 1.26f, 0f);
    private Vector3 rightpositionP3 = new Vector3(7.09f, 1.26f, 0f);
    private float damageMultiplier;
    private BoxCollider hitbox;
    public GameObject ice;
    public GameObject firePrefab;
    public GameObject rockPrefab;
    public GameObject rainPrefab;
    public GameObject rainbowFirePrefab;
    public GameObject ice2;

    public GameObject movingWall1;
    public GameObject movingWall2;
    public GameObject movingWall3;
    public GameObject movingWall4;

    private bool isAttacking = false;
    private bool isTransitioning = false; // Add this line

    public GameObject barrierLeft;
    public GameObject barrierRight;
    private Vector3 barrierLP1 = new Vector3(-15f, 5.13f, 0);
    private Vector3 barrierRP1 = new Vector3(12.03f, 4.69f, -3f);
    private Vector3 barrierLP2 = new Vector3(-19.29f, 6.41f, -0.45f);
    private Vector3 barrierRP2 = new Vector3(16.29f, 6.41f, -0.45f);
    private Vector3 barrierLP3 = new Vector3(-7f, 6.41f, -0.45f);
    private Vector3 barrierRP3 = new Vector3(9.91f, 6.41f, -0.45f);
    private bool phase2Started;
    private bool phase3Started;
    float distanceToPlayer;
    public int phase;
    public phaseManager PhaseManager;


    private List<Coroutine> activeCoroutines = new List<Coroutine>();
    private List<GameObject> activeAttackObjects = new List<GameObject>();

    private void Start()
    {
        PhaseManager.phase1();
        phase = 1;
        transform.position = rightposition;
        barrierLeft.transform.position = barrierLP1;
        barrierRight.transform.position = barrierRP1;
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        if (colliders.Length > 0)
        {
            hitbox = colliders[0];
        }
        FinalEnemyScript = GetComponent<finalEnemyScript>();
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Rotate(enemyRotation);
        if ((FinalEnemyScript.health == 200 || FinalEnemyScript.health == 197.5f || FinalEnemyScript.health == 195 || FinalEnemyScript.health == 192.5f) && !phase2Started)
        {
            phase = 2;
            phase2Started = true;
            StartCoroutine(Phase2Transition());

        }
        else if ((FinalEnemyScript.health == 100 || FinalEnemyScript.health == 97.5f || FinalEnemyScript.health == 95 || FinalEnemyScript.health == 92.5f) && !phase3Started)
        {
            phase = 3;
            phase3Started = true;
            Time.timeScale = 0.5f;
            cancelAttacks();
            barrierLeft.transform.position = barrierLP3;
            barrierRight.transform.position = barrierRP3;
            if (enemyRotation == false)
            {
                transform.position = rightpositionP3;
            }
            else if (enemyRotation == true)
            {
                transform.position = rightposition;
                enemyRotation = false;
            }
            player.transform.position = new Vector3(0, 1, 0);
            chooseAttack();
        }

    }

    void cancelAttacks()
    {
        foreach (var coroutine in activeCoroutines)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
        foreach (var obj in activeAttackObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        activeCoroutines.Clear();
        activeAttackObjects.Clear();
        isAttacking = false;
    }
    IEnumerator Phase2Transition()
    {
        isTransitioning = true; // Set transition flag
        cancelAttacks();
        animator.SetTrigger("earthAttack");
        PhaseManager.phase2Transition();
        yield return new WaitForSeconds(4.5f);
        PhaseManager.phase2();
        barrierLeft.transform.position = barrierLP2;
        barrierRight.transform.position = barrierRP2;
        if (enemyRotation == false)
        {
            transform.position = rightpositionP2;
        }
        else if (enemyRotation == true)
        {
            transform.position = leftpositionP2;
        }
        isTransitioning = false; // Unset transition flag
        animator.SetTrigger("Idle");
        chooseAttack();
    }
    IEnumerator Phase3Transition()
    {
        phase = 3;
        phase3Started = true;
        PhaseManager.phase3Transition();
        yield return new WaitForSeconds(4.3f);
        PhaseManager.phase3();
        yield return new WaitForSeconds(3);
        Time.timeScale = 0.5f;
        cancelAttacks();
        barrierLeft.transform.position = barrierLP3;
        barrierRight.transform.position = barrierRP3;
        if (enemyRotation == false)
        {
            transform.position = rightpositionP3;
        }
        else if (enemyRotation == true)
        {
            transform.position = rightposition;
            enemyRotation = false;
        }
        player.transform.position = new Vector3(0, 1, 0);
        chooseAttack();
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (!isAttacking && !isTransitioning) // Check if not attacking and not transitioning
            {
                chooseAttack();
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }

    void chooseAttack()
    {
        if (isTransitioning) return; // Exit if transitioning

        if (FinalEnemyScript.health > 201)
        {
            string selectedAttack = phaseOneAttacks[rand.Next(phaseOneAttacks.Count)];
            Debug.Log("Enemy performs Phase One " + selectedAttack);
            if (selectedAttack == "fireAttack")
            {
                fireAttack();
            }
            if (selectedAttack == "iceAttack")
            {
                iceAttack();
            }
            if (selectedAttack == "rainAttack")
            {
                rainAttack();
            }
        }
        else if (FinalEnemyScript.health > 101)
        {
            string selectedAttack = phaseTwoAttacks[rand.Next(phaseTwoAttacks.Count)];
            Debug.Log("Enemy performs Phase Two " + selectedAttack);
            if (selectedAttack == "fireAttack")
            {
                phaseTwoFireAttack();
            }
            if (selectedAttack == "iceAttack")
            {
                phaseTwoIceAttack();
            }
            if (selectedAttack == "rainAttack")
            {
                phaseTwoRainAttack();
            }
            if (selectedAttack == "rockAttack")
            {
                phaseTwoRockAttack();
            }
            if (selectedAttack == "ringAttack")
            {
                rockWallAttack();
            }
        }
        else if (FinalEnemyScript.health > 0)
        {
            string selectedAttack = phaseTwoAttacks[rand.Next(phaseTwoAttacks.Count)];
            Debug.Log("Enemy performs Phase Three " + selectedAttack);
            if (selectedAttack == "fireAttack")
            {
                phaseThreeFireAttack();
            }
            if (selectedAttack == "iceAttack")
            {
                PhaseThreeIceAttack();
            }
            if (selectedAttack == "rainAttack")
            {
                phaseThreeFireAttack();
            }
            if (selectedAttack == "rockAttack")
            {
                PhaseThreeIceAttack();
            }
            if (selectedAttack == "ringAttack")
            {
                phaseThreeFireAttack();
            }
        }
    }
    //PHASE THREE ATTACKS
    void phaseThreeFireAttack()
    {
        Coroutine coroutine = StartCoroutine(fire3Numerator());
        activeCoroutines.Add(coroutine);
    }
    void PhaseThreeIceAttack()
    {
        Coroutine coroutine = StartCoroutine(ice3Numerator());
        activeCoroutines.Add(coroutine);
    }




    // PHASE TWO ATTACKS
    void phaseTwoFireAttack()
    {
        Coroutine coroutine = StartCoroutine(fire2Numerator());
        activeCoroutines.Add(coroutine);
    }

    void phaseTwoIceAttack()
    {
        Coroutine coroutine = StartCoroutine(ice2Numerator());
        activeCoroutines.Add(coroutine);
    }

    void phaseTwoRockAttack()
    {
        Coroutine coroutine = StartCoroutine(rockNumerator());
        activeCoroutines.Add(coroutine);
    }

    void phaseTwoRainAttack()
    {
        Coroutine coroutine = StartCoroutine(rain2Numerator());
        activeCoroutines.Add(coroutine);
    }
    void rockWallAttack()
    {
        Coroutine coroutine = StartCoroutine(rockWallNumerator());
        activeCoroutines.Add(coroutine);
    }

    // PHASE ONE ATTACKS
    void rainAttack()
    {
        Coroutine coroutine = StartCoroutine(rainNumerator());
        activeCoroutines.Add(coroutine);
    }
    void fireAttack()
    {
        Coroutine coroutine = StartCoroutine(fireNumerator());
        activeCoroutines.Add(coroutine);
    }

    void iceAttack()
    {
        Coroutine coroutine = StartCoroutine(iceNumerator());
        activeCoroutines.Add(coroutine);
    }

    IEnumerator iceNumerator()
    {
        isAttacking = true;
        animator.SetTrigger("airAttack");
        yield return new WaitForSeconds(1);
        animator.SetBool("inAirAttack", true);
        int totalAttacks = UnityEngine.Random.Range(3, 7);
        int currentAttacks = 0;
        while (currentAttacks < totalAttacks)
        {
            if (enemyRotation == false)
            {
                float waitTime = 1f;

                // Perform the ice attack first
                Vector3 offsetPosition = transform.position + Vector3.right * -1f;
                GameObject iceAttackInstance = Instantiate(ice, offsetPosition, Quaternion.Euler(0, -90, 180));
                activeAttackObjects.Add(iceAttackInstance); // Add to list of active objects
                Destroy(iceAttackInstance, 1f);
                yield return new WaitForSeconds(waitTime); // Wait for the attack to be visible

                currentAttacks++;
                yield return new WaitForSeconds(0.6f); // Wait before teleporting
                transform.position = leftposition;    // Teleport to the left position
                enemyRotation = true;                 // Change the rotation state
                Rotate(enemyRotation);                // Immediately update the rotation
            }
            else if (enemyRotation == true)
            {
                float waitTime = UnityEngine.Random.Range(0.8f, 1f);

                // Perform the ice attack first
                Vector3 offsetPosition = transform.position + Vector3.right * 1f;
                GameObject iceAttackInstance = Instantiate(ice, offsetPosition, Quaternion.Euler(0, 90, 0));
                activeAttackObjects.Add(iceAttackInstance); // Add to list of active objects
                Destroy(iceAttackInstance, 1f);
                yield return new WaitForSeconds(waitTime); // Wait for the attack to be visible

                currentAttacks++;
                yield return new WaitForSeconds(0.6f); // Wait before teleporting
                transform.position = rightposition;   // Teleport to the right position
                enemyRotation = false;                // Change the rotation state
                Rotate(enemyRotation);                // Immediately update the rotation
            }
        }
        animator.SetBool("inAirAttack", false);
        isAttacking = false;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator fireNumerator()
    {
        isAttacking = true;

        int totalAttacks = UnityEngine.Random.Range(1, 5);
        int currentAttacks = 0;

        while (currentAttacks < totalAttacks)
        {
            animator.SetTrigger("fireAttack");
            yield return new WaitForSeconds(0.3f);
            if (enemyRotation == false)
            {

                float waitTime = UnityEngine.Random.Range(1f, 2f);
                Vector3 startingPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                Vector3 offsetPosition = startingPosition + Vector3.right * -1f;
                GameObject fireAttackInstance = Instantiate(firePrefab, offsetPosition, Quaternion.Euler(0, -90, 0));
                activeAttackObjects.Add(fireAttackInstance); // Add to list of active objects
                Destroy(fireAttackInstance, 5f);
                animator.SetTrigger("Idle");
                yield return new WaitForSeconds(waitTime); // Wait for the attack to be 
                currentAttacks++;

                currentAttacks++;
            }
            else if (enemyRotation == true)
            {
                float waitTime = UnityEngine.Random.Range(1f, 2f);
                Vector3 startingPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                Vector3 offsetPosition = transform.position + Vector3.right * 1f;
                GameObject fireAttackInstance = Instantiate(firePrefab, offsetPosition, Quaternion.Euler(0, 90, 0));
                activeAttackObjects.Add(fireAttackInstance); // Add to list of active objects
                Destroy(fireAttackInstance, 5f);
                animator.SetTrigger("Idle");
                yield return new WaitForSeconds(waitTime); // Wait for the attack to be 
                currentAttacks++;
            }
        }
        isAttacking = false;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(2f);
    }

    IEnumerator rainNumerator()
    {
        isAttacking = true;
        int numRain = rand.Next(1, 5);
        isAttacking = false;
        for (int i = 0; i < numRain; i++)
        {
            Vector3 pos = new Vector3(rand.Next(-10, 7), rand.Next(20, 30), 0);
            GameObject rainInstance = Instantiate(rainPrefab, pos, Quaternion.identity);
            activeAttackObjects.Add(rainInstance); // Add to list of active objects
            yield return new WaitForSeconds(2f);
        }
    }


    //P2
    IEnumerator rockWallNumerator()
    {
        isAttacking = true;
        if (enemyRotation == false)
        {
            Vector3 offsetPosition = transform.position + Vector3.right * (enemyRotation ? 1f : -1f);
            bool moveLeft = enemyRotation; // Determine the direction based on enemy rotation

            animator.SetTrigger("earthAttack");
            yield return new WaitForSeconds(0.3f);

            GameObject wall1 = Instantiate(movingWall1, offsetPosition, Quaternion.Euler(0, -90, 0));
            Destroy(wall1, 5f);
            wall1.GetComponent<enemyRockWall>().SetMoveDirection(moveLeft);
            activeAttackObjects.Add(wall1);
            animator.SetTrigger("Idle");
            yield return new WaitForSeconds(1.0f);



            animator.SetTrigger("earthAttack");
            yield return new WaitForSeconds(0.3f);

            GameObject wall2 = Instantiate(movingWall2, offsetPosition, Quaternion.Euler(0, -90, 0));
            Destroy(wall2, 5f);
            wall2.GetComponent<enemyRockWall>().SetMoveDirection(moveLeft);
            activeAttackObjects.Add(wall2);
            animator.SetTrigger("Idle");
            yield return new WaitForSeconds(0.8f);



            animator.SetTrigger("earthAttack");
            yield return new WaitForSeconds(0.3f);


            GameObject wall3 = Instantiate(movingWall3, offsetPosition, Quaternion.Euler(0, -90, 0));
            Destroy(wall3, 5f);

            wall3.GetComponent<enemyRockWall>().SetMoveDirection(moveLeft);
            activeAttackObjects.Add(wall3);
            animator.SetTrigger("Idle");
            yield return new WaitForSeconds(0.6f);



            animator.SetTrigger("earthAttack");
            yield return new WaitForSeconds(0.3f);

            GameObject wall4 = Instantiate(movingWall4, offsetPosition, Quaternion.Euler(0, -90, 0));
            Destroy(wall4, 5f);

            wall4.GetComponent<enemyRockWall>().SetMoveDirection(moveLeft);
            activeAttackObjects.Add(wall4);
            animator.SetTrigger("Idle");

        }
        else
        {

            Vector3 offsetPosition = transform.position + Vector3.right * (enemyRotation ? 1f : -1f);
            bool moveLeft = enemyRotation; // Determine the direction based on enemy rotation

            animator.SetTrigger("earthAttack");

            GameObject wall1 = Instantiate(movingWall1, offsetPosition, Quaternion.Euler(0, 90, 0));
            wall1.GetComponent<enemyRockWall>().SetMoveDirection(moveLeft);
            activeAttackObjects.Add(wall1);
            Destroy(wall1, 5f);

            animator.SetTrigger("Idle");
            yield return new WaitForSeconds(1.0f);



            animator.SetTrigger("earthAttack");
            yield return new WaitForSeconds(0.3f);

            GameObject wall2 = Instantiate(movingWall2, offsetPosition, Quaternion.Euler(0, 90, 0));
            Destroy(wall2, 5f);

            wall2.GetComponent<enemyRockWall>().SetMoveDirection(moveLeft);
            activeAttackObjects.Add(wall2);
            animator.SetTrigger("Idle");
            yield return new WaitForSeconds(0.8f);



            animator.SetTrigger("earthAttack");
            yield return new WaitForSeconds(0.3f);

            GameObject wall3 = Instantiate(movingWall3, offsetPosition, Quaternion.Euler(0, 90, 0));
            Destroy(wall2, 5f);
            wall3.GetComponent<enemyRockWall>().SetMoveDirection(moveLeft);
            activeAttackObjects.Add(wall3);
            animator.SetTrigger("Idle");
            yield return new WaitForSeconds(0.6f);



            animator.SetTrigger("earthAttack");
            yield return new WaitForSeconds(0.3f);

            GameObject wall4 = Instantiate(movingWall4, offsetPosition, Quaternion.Euler(0, 90, 0));
            Destroy(wall4, 5f);
            wall4.GetComponent<enemyRockWall>().SetMoveDirection(moveLeft);
            activeAttackObjects.Add(wall4);
            animator.SetTrigger("Idle");
        }

        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    IEnumerator rain2Numerator()
    {
        isAttacking = true;
        int numRain = rand.Next(10, 20);
        isAttacking = false;
        for (int i = 0; i < numRain; i++)
        {
            Vector3 pos = new Vector3(rand.Next(-18, 14), rand.Next(20, 30), 0);
            GameObject rainInstance = Instantiate(rainPrefab, pos, Quaternion.identity);
            activeAttackObjects.Add(rainInstance); // Add to list of active objects
            yield return new WaitForSeconds(1f);
        }
    }


    IEnumerator ice2Numerator()
    {
        isAttacking = true;
        animator.SetTrigger("airAttack");
        yield return new WaitForSeconds(1);
        animator.SetBool("inAirAttack", true);
        int totalAttacks = UnityEngine.Random.Range(1, 3);
        int swap = UnityEngine.Random.Range(1, 5);
        int currentAttacks = 0;

        while (currentAttacks < totalAttacks)
        {
            float waitTime = UnityEngine.Random.Range(0.8f, 1f);
            Vector3 offsetPosition = transform.position + Vector3.right * (enemyRotation ? 1f : -1f);
            // Perform the ice attacks
            for (int i = 0; i < 3; i++)
            {
                GameObject iceAttackInstance = Instantiate(ice, offsetPosition, Quaternion.Euler(0, enemyRotation ? 90 : -90, 0));
                activeAttackObjects.Add(iceAttackInstance); // Add to list of active objects
                Destroy(iceAttackInstance, 10f);
                yield return new WaitForSeconds(0.3f);
            }

            animator.SetBool("inAirAttack", false);
            currentAttacks++;
            yield return new WaitForSeconds(1f);
            Debug.Log("Swap:" + swap);
            // Update position and 
            if (swap == 1)
            {
                transform.position = enemyRotation ? rightpositionP2 : leftpositionP2;
                enemyRotation = !enemyRotation;
                Rotate(enemyRotation);
            }
        }
        isAttacking = false;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator fire2Numerator()
    {
        isAttacking = true;
        int totalAttacks = UnityEngine.Random.Range(1, 2);
        int currentAttacks = 0;

        while (currentAttacks < totalAttacks)
        {
            float waitTime = UnityEngine.Random.Range(1f, 3f);
            Vector3 startingPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Vector3 offsetPosition = startingPosition + Vector3.right * (enemyRotation ? 1f : -1f);

            // Perform the fire attacks
            for (int i = 0; i < 3; i++)
            {
                animator.SetTrigger("fireAttack");
                yield return new WaitForSeconds(0.4f);
                GameObject fireAttackInstance = Instantiate(firePrefab, offsetPosition, Quaternion.Euler(0, enemyRotation ? 90 : -90, 0));
                activeAttackObjects.Add(fireAttackInstance); // Add to list of active objects
                Destroy(fireAttackInstance, 20f);
                animator.SetTrigger("Idle");
                yield return new WaitForSeconds(2f);
            }

            currentAttacks++;
            yield return new WaitForSeconds(2);

        }
        isAttacking = false;
        animator.SetTrigger("Idle");

    }

    IEnumerator rockNumerator()
    {
        animator.SetTrigger("earthAttack");
        isAttacking = true;
        Vector3 playerInitialPosition = player.transform.position;

        Vector3 rockposition = new Vector3(transform.position.x, -1.5f, transform.position.z);
        GameObject rockInstance = Instantiate(rockPrefab, rockposition, Quaternion.identity);
        activeAttackObjects.Add(rockInstance); // Add to list of active objects

        float duration = 2f;
        float elapsedTime = 0f;
        Vector3 startPosition = rockInstance.transform.position;
        Vector3 endPosition = new Vector3(playerInitialPosition.x, -10f, playerInitialPosition.z);
        isAttacking = false;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float height = 10f * Mathf.Sin(Mathf.PI * t); // Adjust the height of the arc
            rockInstance.transform.position = Vector3.Lerp(startPosition, endPosition, t) + Vector3.up * height;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the rock reaches the exact end position
        rockInstance.transform.position = endPosition;
        animator.SetTrigger("Idle");
        Destroy(rockInstance, 1f);
        yield return new WaitForSeconds(1f);
    }
    //P3

    IEnumerator fire3Numerator()
    {
        isAttacking = true;

        int totalAttacks = UnityEngine.Random.Range(1, 5);
        int currentAttacks = 0;

        while (currentAttacks < totalAttacks)
        {
            animator.SetTrigger("fireAttack");
            yield return new WaitForSecondsRealtime(0.3f);
            if (enemyRotation == false)
            {

                float waitTime = UnityEngine.Random.Range(1f, 2f);
                Vector3 startingPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                Vector3 offsetPosition = startingPosition + Vector3.right * -1f;
                GameObject fireAttackInstance = Instantiate(rainbowFirePrefab, offsetPosition, Quaternion.Euler(0, -90, 0));
                activeAttackObjects.Add(fireAttackInstance); // Add to list of active objects
                Destroy(fireAttackInstance, 5f);
                animator.SetTrigger("Idle");
                yield return new WaitForSecondsRealtime(2); // Wait for the attack to be 
                currentAttacks++;

                currentAttacks++;
            }
            else if (enemyRotation == true)
            {
                float waitTime = UnityEngine.Random.Range(1f, 2f);
                Vector3 startingPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                Vector3 offsetPosition = transform.position + Vector3.right * 1f;
                GameObject fireAttackInstance = Instantiate(rainbowFirePrefab, offsetPosition, Quaternion.Euler(0, 90, 0));
                activeAttackObjects.Add(fireAttackInstance); // Add to list of active objects
                Destroy(fireAttackInstance, 5f);
                animator.SetTrigger("Idle");
                yield return new WaitForSecondsRealtime(2); // Wait for the attack to be 
                currentAttacks++;
            }
        }
        isAttacking = false;
        animator.SetTrigger("Idle");
        yield return new WaitForSecondsRealtime(1f);
    }

    IEnumerator ice3Numerator()
    {
        isAttacking = true;
        animator.SetTrigger("airAttack");
        yield return new WaitForSecondsRealtime(0.5f);
        animator.SetBool("inAirAttack", true);
        int totalAttacks = UnityEngine.Random.Range(3, 7);
        int currentAttacks = 0;
        while (currentAttacks < totalAttacks)
        {
            if (enemyRotation == false)
            {
                float waitTime = 1f;

                // Perform the ice attack first
                Vector3 offsetPosition = transform.position + Vector3.right * -1f;
                GameObject iceAttackInstance = Instantiate(ice2, offsetPosition, Quaternion.Euler(0, -90, 180));
                activeAttackObjects.Add(iceAttackInstance); // Add to list of active objects
                Destroy(iceAttackInstance, 1f);
                yield return new WaitForSecondsRealtime(0.5f); // Wait for the attack to be visible

                currentAttacks++;
                yield return new WaitForSecondsRealtime(0.6f); // Wait before teleporting
                transform.position = leftpositionP3;    // Teleport to the left position
                enemyRotation = true;                 // Change the rotation state
                Rotate(enemyRotation);                // Immediately update the rotation
            }
            else if (enemyRotation == true)
            {
                float waitTime = UnityEngine.Random.Range(0.8f, 1f);

                // Perform the ice attack first
                Vector3 offsetPosition = transform.position + Vector3.right * 1f;
                GameObject iceAttackInstance = Instantiate(ice2, offsetPosition, Quaternion.Euler(0, 90, 0));
                activeAttackObjects.Add(iceAttackInstance); // Add to list of active objects
                Destroy(iceAttackInstance, 1f);
                yield return new WaitForSecondsRealtime(0.5f); // Wait for the attack to be visible

                currentAttacks++;
                yield return new WaitForSecondsRealtime(0.6f); // Wait before teleporting
                transform.position = rightpositionP3;   // Teleport to the right position
                enemyRotation = false;                // Change the rotation state
                Rotate(enemyRotation);                // Immediately update the rotation
            }
        }
        isAttacking = false;
        yield return new WaitForSecondsRealtime(0.5f);
    }
        IEnumerator earthWallCoroutine()
    {
        hitbox.enabled = false;
        yield return new WaitForSeconds(1.5f);
        hitbox.enabled = true;
    }

    void Rotate(bool enemyRotation)
    {
        if (enemyRotation)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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
            earthWall EarthWall = collision.gameObject.GetComponent<earthWall>();
            EarthWall.increaseHits();
            Debug.Log(EarthWall.hitstaken);
            StartCoroutine(earthWallCoroutine());
        }
    }
}

