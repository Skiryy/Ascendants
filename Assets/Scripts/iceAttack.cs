// FireAttack.cs
using System.Runtime.CompilerServices;
using UnityEngine;

public class iceAttack : MonoBehaviour
{
    public float speed = 15f;
    private void Start()
    {
    }
    void Update()
    {
        // Move the fire attack forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider collision)
    {
        // Destroy the fire attack when it collides with something
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("test");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 9)
        {
            enemyScript EnemyScript = collision.gameObject.GetComponent<enemyScript>();
            EnemyScript.health -= 1f;

        }
        Destroy(gameObject);
    }
}
