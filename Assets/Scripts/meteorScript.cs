using System.Runtime.CompilerServices;
using UnityEngine;

public class meteorScript : MonoBehaviour
{
    public float speed = 7f;

    void Update()
    {
        // Move the meteor downwards
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Destroy the meteor when it collides with something
        Destroy(gameObject);
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("test");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 3)
        {
            playerHealthScript PlayerHealthScript = collision.gameObject.GetComponent<playerHealthScript>();
            PlayerHealthScript.hit();
            Destroy(gameObject);
        }
    }
}
