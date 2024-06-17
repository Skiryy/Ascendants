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
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider collision)
    {
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
