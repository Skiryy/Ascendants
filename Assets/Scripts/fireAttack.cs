// FireAttack.cs
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Move the fire attack forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter (Collider collision)
    {
        Destroy(gameObject);
        // Destroy the fire attack when it collides with something
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("test");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 9)
        {
            enemyScript EnemyScript = collision.gameObject.GetComponent<enemyScript>();
            EnemyScript.health -= 10f;
        }
        if (collision.gameObject.layer == 14)
        {
            dragonEnemyScript DragonEnemyScript = collision.gameObject.GetComponent<dragonEnemyScript>();
            DragonEnemyScript.health -= 10f;
        }
        if (collision.gameObject.layer == 11)
        {
            Destroy(collision.gameObject);
        }
    }
}
