using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class finalEnemyActions : MonoBehaviour
{
    public Animator animator;
    private finalEnemyScript FinalEnemyScript;
    public GameObject player;
    private List<string> phaseOneAttacks = new List<string> { "iceAttack", "fireAttack" };
    private List<string> phaseTwoAttacks = new List<string> { "iceAttack", "fireAttack", "waveAttack", "rockAttack", "ringAttack" };

    private System.Random rand = new System.Random();
    private float attackCooldown = 10f;
    private float lastAttackTime = 0f;
    private bool enemyRotation = false;
    private Vector3 leftposition = new Vector3(-10.24f, 1.26f, 0f);
    private Vector3 rightposition = new Vector3(7.09f, 1.26f, 0f);
    private Vector3 leftpositionP2 = new Vector3(-22f, 1.26f, 0f);
    private Vector3 rightpositionP2 = new Vector3(19f, 1.26f, 0f);
    private float damageMultiplier;
    private BoxCollider hitbox;
    public GameObject ice;
    public GameObject firePrefab;
    public GameObject rockPrefab;
    private bool isAttacking = false;
    //barriers
    public GameObject barrierLeft;
    public GameObject barrierRight;
    private Vector3 barrierLP1 = new Vector3(-12.29f, 6.41f, -0.45f);
    private Vector3 barrierRP1 = new Vector3(9.91f, 6.41f, -0.45f);
    private Vector3 barrierLP2 = new Vector3(-24.29f, 6.41f, -0.45f);
    private Vector3 barrierRP2 = new Vector3(21.29f, 6.41f, -0.45f);
    private bool phase2Started;
    private GameObject rainPrefab;

    private List<Coroutine> activeCoroutines = new List<Coroutine>();
    private List<GameObject> activeAttackObjects = new List<GameObject>();

    private void Start()
    {
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
        Rotate(enemyRotation);
        if (FinalEnemyScript.health == 200 && !phase2Started)
        {
            phase2Started = true;
            cancelAttacks();
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

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (!isAttacking)
            {
                chooseAttack();
            }
            yield return new WaitForSeconds(1);
        }
    }

    void chooseAttack()
    {
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
            if (selectedAttack == "waveAttack")
            {
                phaseTwoRainAttack();
            }
            if (selectedAttack == "rockAttack")
            {
                phaseTwoRockAttack();
            }
            if (selectedAttack == "ringAttack")
            {
                phaseTwoFireAttack();
            }
        }
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
        Coroutine coroutine = StartCoroutine(rainNumerator());
        activeCoroutines.Add(coroutine);
    }

    // PHASE ONE ATTACKS

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
        yield return new WaitForSeconds(1);
        // Trigger ice animation
        int totalAttacks = UnityEngine.Random.Range(5, 15);
        int currentAttacks = 0;

        while (currentAttacks < totalAttacks)
        {
            if (enemyRotation == false)
            {
                float waitTime = UnityEngine.Random.Range(0.8f, 1f);

                // Perform the ice attack first
                Vector3 offsetPosition = transform.position + Vector3.right * -1f;
                GameObject iceAttackInstance = Instantiate(ice, offsetPosition, Quaternion.Euler(0, -90, 0));
                activeAttackObjects.Add(iceAttackInstance); // Add to list of active objects
                Destroy(iceAttackInstance, 1f);
                yield return new WaitForSeconds(waitTime); // Wait for the attack to be visible

                currentAttacks++;
                yield return new WaitForSeconds(0.4f); // Wait before teleporting
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
                yield return new WaitForSeconds(0.4f); // Wait before teleporting
                transform.position = rightposition;   // Teleport to the right position
                enemyRotation = false;                // Change the rotation state
                Rotate(enemyRotation);                // Immediately update the rotation
            }
        }
        isAttacking = false;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator fireNumerator()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1);
        // Trigger ice animation
        int totalAttacks = UnityEngine.Random.Range(1, 5);
        int currentAttacks = 0;

        while (currentAttacks < totalAttacks)
        {
            if (enemyRotation == false)
            {
                float waitTime = UnityEngine.Random.Range(1f, 3f);

                // Perform the ice attack first
                Vector3 offsetPosition = transform.position + Vector3.right * -1f;
                GameObject fireAttackInstance = Instantiate(firePrefab, offsetPosition, Quaternion.Euler(0, -90, 0));
                activeAttackObjects.Add(fireAttackInstance); // Add to list of active objects
                Destroy(fireAttackInstance, 5f);
                yield return new WaitForSeconds(waitTime); // Wait for the attack to be visible

                currentAttacks++;
            }
            else if (enemyRotation == true)
            {
                float waitTime = UnityEngine.Random.Range(1f, 3f);

                // Perform the ice attack first
                Vector3 offsetPosition = transform.position + Vector3.right * 1f;
                GameObject fireAttackInstance = Instantiate(firePrefab, offsetPosition, Quaternion.Euler(0, 90, 0));
                activeAttackObjects.Add(fireAttackInstance); // Add to list of active objects
                Destroy(fireAttackInstance, 5f);
                yield return new WaitForSeconds(waitTime); // Wait for the attack to be visible

                currentAttacks++;
            }
        }
        isAttacking = false;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator rainNumerator()
    {
        int numRain = rand.Next(30, 40);

        for (int i = 0; i < numRain; i++)
        {
            Vector3 pos = new Vector3(rand.Next(-30, 30), 40, 0);
            GameObject rockInstance = Instantiate(rainPrefab, pos, Quaternion.identity);
            activeAttackObjects.Add(rockInstance); // Add to list of active objects
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ice2Numerator()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1);
        // Trigger ice animation
        int totalAttacks = UnityEngine.Random.Range(1, 3);
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
                Destroy(iceAttackInstance, 1f);
                yield return new WaitForSeconds(0.3f);
            }

            yield return new WaitForSeconds(waitTime);

            currentAttacks++;
            yield return new WaitForSeconds(0.4f);

            // Update position and rotation
            transform.position = enemyRotation ? rightpositionP2 : leftpositionP2;
            enemyRotation = !enemyRotation;
            Rotate(enemyRotation);
        }
        isAttacking = false;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator fire2Numerator()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1);
        // Trigger fire animation
        int totalAttacks = UnityEngine.Random.Range(1, 2);
        int currentAttacks = 0;

        while (currentAttacks < totalAttacks)
        {
            float waitTime = UnityEngine.Random.Range(1f, 3f);
            Vector3 offsetPosition = transform.position + Vector3.right * (enemyRotation ? 1f : -1f);

            // Perform the fire attacks
            for (int i = 0; i < 3; i++)
            {
                GameObject fireAttackInstance = Instantiate(firePrefab, offsetPosition, Quaternion.Euler(0, enemyRotation ? 90 : -90, 0));
                activeAttackObjects.Add(fireAttackInstance); // Add to list of active objects
                Destroy(fireAttackInstance, 5f);
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(waitTime);

            currentAttacks++;

        }
        isAttacking = false;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator rockNumerator()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1);

        // Get player's initial position
        Vector3 playerInitialPosition = player.transform.position;

        Vector3 rockposition = new Vector3(transform.position.x, -1.5f, transform.position.z);
        GameObject rockInstance = Instantiate(rockPrefab, rockposition, Quaternion.identity);
        activeAttackObjects.Add(rockInstance); // Add to list of active objects

        // Define the arc parameters
        float duration = 2f;
        float elapsedTime = 0f;
        Vector3 startPosition = rockInstance.transform.position;
        Vector3 endPosition = new Vector3(playerInitialPosition.x, -1.5f, playerInitialPosition.z);

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

        Destroy(rockInstance, 1f);

        isAttacking = false;
        yield return new WaitForSeconds(2f);
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
