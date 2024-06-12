using System.Runtime.CompilerServices;
using UnityEngine;

public class meteorScript : MonoBehaviour
{
    public float speed = 7f;
    public Sprite[] sprites; 
    public GameObject spriteObject;
    public SpriteRenderer spriteRenderer;

    void Start()
    {

        spriteObject = new GameObject("Sprite");
        spriteObject.transform.parent = transform;
        spriteObject.transform.localPosition = Vector3.zero;


        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];


        spriteObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }

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
            PlayerHealthScript.hit();
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
            Debug.Log("hi");
        }
    }
}
