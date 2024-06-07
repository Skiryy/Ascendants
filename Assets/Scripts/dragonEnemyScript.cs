using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dragonEnemyScript : MonoBehaviour
{
    public float health = 100f;
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
