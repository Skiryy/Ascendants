using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    private enemyScript EnemyScript;
    public GameObject bullet;
    public GameObject normalAnimation;
    public GameObject flyingAnimation;
    public GameObject jumpAnimation;
    public GameObject player;
    public GameObject barrel;
    public GameObject lazerVisualiser;
    private List<string> attacks = new List<string> { "BarrelMoving", "Jump", "Wait" };
    private System.Random rand = new System.Random();
    private float attackCooldown = 10f;
    private float lastAttackTime = 0f;
    private bool tankRotation = true;
    private Vector3 leftposition = new Vector3(-12.39f, 0.24f, 0.45f);
    private float damageMultiplier;
    private BoxCollider hitbox;
    private BoxCollider hitbox2;

    private void Start()
    {
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        hitbox = colliders[0];
        hitbox2 = colliders[1];
        EnemyScript = GetComponent<enemyScript>();
        chooseAttack();
    }
    void chooseAttack()
    {
        string selectedAttack = attacks[rand.Next(attacks.Count)];
        Debug.Log("Enemy performs " + selectedAttack);
        if (selectedAttack == "BarrelMoving")
        {
            barrelmovingAttack();
        }
        if (selectedAttack == "Jump")
        {
            jumpAttack();
        }
        if (selectedAttack == "Wait")
        {
            waitAttack();
         }
    }

    void jumpAttack()
    {
        StartCoroutine(jumpAttackCoroutine());
    }
    void waitAttack()
    {
        StartCoroutine(waitAttackCoroutine());
    }

    void barrelmovingAttack()
    {
        StartCoroutine(BarrelMovingCoroutine());
    }


    IEnumerator jumpAttackCoroutine()
    {
        float verticalRiseDuration = 0.5f;
        float verticalPauseDuration = 0.8f;
        float returnDuration = 3f; 
        float jumpAmount = Random.Range(1, 5);

        Vector3 startPosition = transform.position;
        while (jumpAmount > 0)
        {
            Vector3 currentPosition = transform.position;

            Vector3 targetPositionAbovePlayer = new Vector3(player.transform.position.x, 5, player.transform.position.z);

            animator.SetTrigger("tankJumpLoading");
            barrel.SetActive(false);
            yield return new WaitForSeconds(1f);


            animator.SetTrigger("Hover");
            yield return new WaitForSeconds(1f);
            float elapsedTime = 0f;
            while (elapsedTime < verticalRiseDuration)
            {
                float t = elapsedTime / verticalRiseDuration;
                transform.position = Vector3.Lerp(currentPosition, targetPositionAbovePlayer, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }



            yield return new WaitForSeconds(verticalPauseDuration);


            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            animator.SetTrigger("tankFall"); 
            yield return new WaitForSeconds(1); 
            animator.SetTrigger("tankIdle");
            barrel.SetActive(true);
            jumpAmount -= 1;
        }
        Vector3 startPositionSlamDown = transform.position; 
        float elapsedTimeReturn = 0f;
        while (elapsedTimeReturn < returnDuration)
        {
            float tReturn = elapsedTimeReturn / returnDuration;
            transform.position = Vector3.Lerp(startPositionSlamDown, startPosition, tReturn);
            elapsedTimeReturn += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition; 


        Debug.Log("Finished");
        chooseAttack();
    }

    IEnumerator BarrelMovingCoroutine()
    {
        float attackDuration = 15f;
        float moveSpeed = 0.8f; 
        float fireRate = 0.8f; 

        float startTime = Time.time;

        while (Time.time - startTime < attackDuration)
        {
            damageMultiplier = 1f;
            float randomY = UnityEngine.Random.Range(0.8f, 2.7f);
            barrel.transform.position = new Vector3(barrel.transform.position.x, randomY, barrel.transform.position.z);
            int direction = (tankRotation) ? 1 : -1;
            Vector3 barrelPosition = barrel.transform.position;
            Vector3 offsetPosition = barrelPosition + Vector3.right * 1f * direction;

            if (Time.time - lastAttackTime >= fireRate)
            {
                lastAttackTime = Time.time;
                if (tankRotation == true)
                {
                    GameObject tankBulletInstance = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 90, -90));
                    Destroy(tankBulletInstance, 5f);
                    yield return new WaitForSeconds(0.5f);
                    GameObject tankBulletInstance2 = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 90, -90));
                    Destroy(tankBulletInstance2, 5f);
                    yield return new WaitForSeconds(0.5f);
                    GameObject tankBulletInstance3 = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 90, -90));
                    Destroy(tankBulletInstance3, 5f);
                }
                else
                {
                    GameObject tankBulletInstance = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 0, 90));
                    Destroy(tankBulletInstance, 5f);
                    yield return new WaitForSeconds(0.5f);
                    GameObject tankBulletInstance2 = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 0, 90));
                    Destroy(tankBulletInstance2, 5f);
                    yield return new WaitForSeconds(0.5f);
                    GameObject tankBulletInstance3 = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 0, 90));
                    Destroy(tankBulletInstance3, 5f);
                }
            }

            yield return new WaitForSeconds(moveSpeed);
        }

        barrel.transform.position = new Vector3(barrel.transform.position.x, 2.1f, barrel.transform.position.z);
        Debug.Log("Finito");
        chooseAttack();
    }
    IEnumerator waitAttackCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        chooseAttack();
    }
    IEnumerator earthWallCoroutine()
    {
        hitbox.enabled = false;
        hitbox2.enabled = false;

        yield return new WaitForSeconds(1.5f);
        hitbox.enabled = true;
        hitbox2.enabled = true;

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
