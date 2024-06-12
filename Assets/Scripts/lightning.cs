using System.Collections;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private bool inABox = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // Check if collided with the box
        {
            Debug.Log("in a box");
            inABox = true;
            Destroy(gameObject); // Destroy the lightning object when it hits the box
        }
        if (other.gameObject.layer == 3) // Check if collided with the player
        {
            playerHealthScript PlayerHealthScript = other.gameObject.GetComponent<playerHealthScript>();
            StartCoroutine(healthCheck(PlayerHealthScript));
        }
    }
    IEnumerator healthCheck(playerHealthScript PlayerHealthScript)
    {
        Debug.Log("Started coroutine");
        yield return new WaitForSeconds(0.1f);
        if (!inABox) {
            Debug.Log("not in a box");
            PlayerHealthScript.rocketHit();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("In a box");
        }
    }
 
}
