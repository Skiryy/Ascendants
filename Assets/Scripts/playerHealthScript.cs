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

    void Start()
    {

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
        StartCoroutine(GettingHit());
    }

    IEnumerator GettingHit()
    {
        bool stun = true;
        health -= 20f;
        hitbox.enabled = false;

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        float startTime = Time.time;
        float stunDuration = 2f;

        while (Time.time < startTime + stunDuration)
        {
            //stun
            yield return null;
        }

        stun = false;
        yield return new WaitForSeconds(1);
        hitbox.enabled = true; // Reactivate hitbox after stun
    }
}
