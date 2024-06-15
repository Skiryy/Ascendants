using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBRock : MonoBehaviour
{
    public GameObject spriteRenderer;
    public float rotationSpeed = 500f;

    void Start()
    {
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            spriteRenderer.transform.Rotate(0, 0, rotationAmount);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            earthWall EarthWall = collision.gameObject.GetComponent<earthWall>();
            EarthWall.increaseHits();
            gameObject.SetActive(false);
        }
        if (collision.gameObject.layer == 3)
        {
            playerHealthScript PlayerHealthScript = collision.gameObject.GetComponent<playerHealthScript>();
            PlayerHealthScript.hit();
            gameObject.SetActive(false);
        }
    }
}
