using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerHealthScript : MonoBehaviour
{
    public float health = 100f;
    private CharacterMover characterMover;
    public TextMeshProUGUI enemyHealthDisplay;
    public Collider hitbox;
    public Collider hitbox2;
    private Rigidbody rb;
    private float damageMultiplier = 1f;
    public Animator animator;
    public bool stunned;
    public bool notAgain;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        enemyHealthDisplay.text = "Player Health" + health;
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Dead");
    }

    public void hit()
    {
        if (notAgain == false) {
     StartCoroutine(GettingHit());
        }
    }
    public void rocketHit()
    {
        if (notAgain == false)
        {
            StartCoroutine(rocketHitted());
        }
    }

    IEnumerator GettingHit()
    {
        notAgain = true;
        stunned = true;
        health -= (20f * damageMultiplier);

        float startTime = Time.time;
        float stunDuration = 2f;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        while (Time.time < startTime + stunDuration)
        {
            damageMultiplier = 0f;
            yield return null;
        }
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        stunned = false;

        yield return new WaitForSeconds(1);
        notAgain = false;
        damageMultiplier = 1f;

    }
    IEnumerator rocketHitted()
    {
        notAgain = true;
        stunned = true;
        health -= (5f * damageMultiplier);
        float startTime = Time.time;
        float stunDuration = 3f;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        while (Time.time < startTime + stunDuration)
        {
            damageMultiplier = 0f;
            yield return null;
        }
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        stunned = false;
        yield return new WaitForSeconds(1);
        notAgain = false;
        damageMultiplier = 1f;


    }
}
