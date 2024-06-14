using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBRock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    { 
        // Destroy the fire attack when it collides with something
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("test");
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
