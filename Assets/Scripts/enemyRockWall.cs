using UnityEngine;

public class enemyRockWall : MonoBehaviour
{
    public float speed = 5f;
    private bool moveLeft;

    // Function to set the movement direction
    public void SetMoveDirection(bool isMovingLeft)
    {
        moveLeft = isMovingLeft;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider collision)
    {
        // Destroy the rock wall when it collides with something
        if (collision.gameObject.layer == 7)
        {
            earthWall EarthWall = collision.gameObject.GetComponent<earthWall>();
            EarthWall.increaseHits();
        }
        if (collision.gameObject.layer == 3)
        {
            playerHealthScript PlayerHealthScript = collision.gameObject.GetComponent<playerHealthScript>();
            PlayerHealthScript.hit();
        }
        if (collision.gameObject.layer == 6)
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
