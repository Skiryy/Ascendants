using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //load full heart
    }

    void Update()
    {
        enemyHealthDisplay.text = "Player Health: " + health;
        if (health == 50)
        {
            //load half image
        }
        else if (health <= 0)
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
        health -= (100f);

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
