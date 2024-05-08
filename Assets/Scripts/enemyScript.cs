using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Dit wordt toegevoegd omdat de text functie van unity zo heet.

public class enemyScript : MonoBehaviour
{
    public float health = 100f;
    public GameObject barrel;
    public TextMeshProUGUI enemyHealthDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthDisplay.text = "Enemy Health" + health; 
        if (health <= 0)
        {
            Death();
        }
        
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
