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
        enemyHealthDisplay.text = "Player Health: " + health;
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
        health -= (20f * damageMultiplier);

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
        SetCharacterOpacity(1f);

        yield return new WaitForSeconds(2f);
        notAgain = false;
        damageMultiplier = 1f;
    }

    IEnumerator rocketHitted()
    {
        notAgain = true;
        stunned = true;
        health -= (5f * damageMultiplier);

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
        SetCharacterOpacity(1f);

        yield return new WaitForSeconds(2f);
        notAgain = false;
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
