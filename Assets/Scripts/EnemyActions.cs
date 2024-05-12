using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private enemyScript EnemyScript;
    public GameObject bullet;
    public GameObject normalAnimation;
    public GameObject flyingAnimation;
    public GameObject jumpAnimation;
    public GameObject player;
    public GameObject barrel;
    private List<string> attacks = new List<string> { "BarrelMoving", "Jump", "Dash", "Wait" };
    private System.Random rand = new System.Random();
    private float attackCooldown = 10f;
    private float lastAttackTime = 0f;
    private bool tankRotation = true;
    private Vector3 leftposition = new Vector3(-12.39f, 0.24f, 0.45f);
    private float damageMultiplier;

    private void Start()
    {
        EnemyScript = GetComponent<enemyScript>();
        chooseAttack();

    }

    void Update()
    {
        // Update logic here if needed
    }

    void chooseAttack()
    {
        string selectedAttack = attacks[rand.Next(attacks.Count)];
        Debug.Log("Enemy performs " + selectedAttack);
        if (selectedAttack == "BarrelMoving")
        {
            barrelmovingAttack();
        }
        if (selectedAttack == "Dash")
        {
            jumpAttack();
        }
        if (selectedAttack == "Jump")
        {
            jumpAttack();
        }
        if (selectedAttack == "Wait")
        {
            jumpAttack();
        }
        // Implement logic for other attacks (Jump, Dash, Wait) here
    }
    void jumpAttack()
    {
        StartCoroutine(jumpAttackCoroutine());
    }    
    void barrelmovingAttack()
    {
        StartCoroutine(BarrelMovingCoroutine());
    }
    IEnumerator jumpAttackCoroutine()
    {
        float jumpAmount = 2;
        float currentJumps = 0;
        while (currentJumps < jumpAmount)
        {
            normalAnimation.gameObject.SetActive(false);
            flyingAnimation.SetActive(false);
            jumpAnimation.gameObject.SetActive(true);
            barrel.SetActive(false);
            yield return new WaitForSeconds(4);
            Vector3 newPositionUp = new Vector3(player.transform.position.x, 5, player.transform.position.z);
            Vector3 newPositionDown = new Vector3(player.transform.position.x, 0 , player.transform.position.z);
            damageMultiplier = 0f;
            transform.position = newPositionUp;
            flyingAnimation.gameObject.SetActive(true);
            jumpAnimation.gameObject.SetActive(false);
            barrel.SetActive(false);
            yield return new WaitForSeconds(2);
            damageMultiplier = 1f;
            transform.position = newPositionDown;
            currentJumps += 1;
            normalAnimation.gameObject.SetActive(true);
            jumpAnimation.gameObject.SetActive(false);
            flyingAnimation.SetActive(false);
            barrel.SetActive(true);
        }
        normalAnimation.gameObject.SetActive(true);
        jumpAnimation.gameObject.SetActive(false);
        flyingAnimation.SetActive(false);
        barrel.SetActive(true);
        yield return new WaitForSeconds(5);
        transform.position = leftposition;
        Debug.Log("Finished");
        chooseAttack();
    }
    IEnumerator BarrelMovingCoroutine()
    {
        float attackDuration = 10f; // Set the attack duration
        float moveSpeed = 1f; // Adjust the speed as needed
        float fireRate = 1f; // Adjust the fire rate as needed

        // Store the start time of the attack
        float startTime = Time.time;

        while (Time.time - startTime < attackDuration)
        {
            damageMultiplier = 1f;
            float randomY = UnityEngine.Random.Range(0.8f, 2.7f); // Adjust as needed
            barrel.transform.position = new Vector3(barrel.transform.position.x, randomY, barrel.transform.position.z);
            int direction = (tankRotation) ? 1 : -1;
            Vector3 barrelPosition = barrel.transform.position;
            Vector3 offsetPosition = barrelPosition + Vector3.right * 2.9f * direction;

            if (Time.time - lastAttackTime >= fireRate)
            {
                lastAttackTime = Time.time;
                if (tankRotation == true)
                {
                    GameObject tankBulletInstance = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 90, -90));
                    Destroy(tankBulletInstance, 5f);
                    yield return new WaitForSeconds(0.5f);
                    GameObject tankBulletInstance2 = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 90, -90));
                    Destroy(tankBulletInstance2, 2f);
                    yield return new WaitForSeconds(0.5f);
                    GameObject tankBulletInstance3 = Instantiate(bullet, offsetPosition, Quaternion.Euler(0, 90, -90));
                    Destroy(tankBulletInstance3, 2f);
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
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 3)
        {
            playerHealthScript PlayerHealthScript = collision.gameObject.GetComponent<playerHealthScript>();
            PlayerHealthScript.health -= (10f * damageMultiplier);
            Debug.Log("Player hit");

        }
        if (collision.gameObject.layer == 7) {
            Destroy(collision.gameObject);
                }
    }
}
