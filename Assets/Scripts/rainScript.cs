using System.Runtime.CompilerServices;
using UnityEngine;

public class rainScript : MonoBehaviour
{
    public float speed = 7f;

    void Update()
    {

        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.layer == 7)
        {
            Debug.Log("test");
            Destroy(gameObject);
            earthWall EarthWall = collision.gameObject.GetComponent<earthWall>();
            EarthWall.increaseHits();
        }
        else if (collision.gameObject.layer == 3)
        {
            playerHealthScript PlayerHealthScript = collision.gameObject.GetComponent<playerHealthScript>();
            PlayerHealthScript.rocketHit();
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
            Debug.Log("hi");
        }
    }
}
