using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class playerHealthScript : MonoBehaviour
{
    public float health = 100f;
    private CharacterMover characterMover;
    public TextMeshProUGUI enemyHealthDisplay;
    //get the 2 images
    public Collider hitbox;
    public Collider hitbox2;
    private Rigidbody rb;
    private float damageMultiplier = 1f;
    public Animator animator;
    public bool stunned;
    public bool notAgain;
    public GameObject fullHeart;
    public GameObject halfHeart;
    public GameObject deathScreen;

    void Start()
    {
        health = GameData.playerHealth;
        rb = GetComponent<Rigidbody>();
        halfHeart.SetActive(false);
        fullHeart.SetActive(true);
    }

    void Update()
    {
        enemyHealthDisplay.text = "Player Health: " + health;
        if (health == 50)
        {
            halfHeart.SetActive(true);
            fullHeart.SetActive(false);
        }
        else if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        StartCoroutine(deathScreenNumerator());
        Debug.Log("Dead");
    }
    IEnumerator deathScreenNumerator()
    {
        Time.timeScale = 0.15f;
        yield return new WaitForSecondsRealtime(1.5f);
        deathScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        GameData.playerHealth = 100f;
        SceneManager.LoadScene(0);
    }

    public void hit()
    {
        if (!notAgain)
        {
            StartCoroutine(GettingHit());
        }
    }

    public void rocketHit()
    {
        if (!notAgain)
        {
            StartCoroutine(rocketHitted());
        }
    }

    IEnumerator GettingHit()
    {
        notAgain = true;
        stunned = true;
        health -= (100f);

        SetCharacterOpacity(0.5f);

        hitbox.enabled = false;
        hitbox2.enabled = false;

        rb.constraints = RigidbodyConstraints.FreezeAll;
        damageMultiplier = 0f;

        yield return new WaitForSeconds(1f);

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        stunned = false;

        hitbox.enabled = true;
        hitbox2.enabled = true;


        yield return new WaitForSeconds(2f);
        SetCharacterOpacity(1f);
        notAgain = false;
        damageMultiplier = 1f;
    }

    IEnumerator rocketHitted()
    {
        notAgain = true;
        stunned = true;
        health -= (50f);

        // Change the character's opacity
        SetCharacterOpacity(0.5f);

        // Disable hitbox colliders
        hitbox.enabled = false;
        hitbox2.enabled = false;

        rb.constraints = RigidbodyConstraints.FreezeAll;
        damageMultiplier = 0f;

        // Stun duration
        yield return new WaitForSeconds(1f);

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        stunned = false;

        // Enable hitbox colliders
        hitbox.enabled = true;
        hitbox2.enabled = true;

        // Reset character opacity after stun duration

        yield return new WaitForSeconds(2f);
        notAgain = false;
        SetCharacterOpacity(1f);
        damageMultiplier = 1f;
    }

    // Function to set character opacity
    void SetCharacterOpacity(float opacity)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            color.a = opacity;
            renderer.material.color = color;
        }
    }
}
