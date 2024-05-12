// FireAttack.cs
using System.Runtime.CompilerServices;
using UnityEngine;

public class tankBullet : MonoBehaviour
{
    public float speed = 7f;

    void Update()
    {
        // Move the fire attack forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
        // Destroy the fire attack when it collides with something
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("test");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 3)
        {
            playerHealthScript playerHealthScript = collision.gameObject.GetComponent<playerHealthScript>();
            playerHealthScript.health -= 10f;
            Destroy(gameObject);
        }
    }
}
