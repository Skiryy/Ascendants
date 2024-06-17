using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class dragonEnemyScript : MonoBehaviour
{
    public float health = 200f;
    public GameObject barrel;
    public TextMeshProUGUI enemyHealthDisplay;
    public List<Image> fullHeartImages; 
    public sceneManager scenes;


    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthDisplay();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthDisplay();
        if (health <= 0)
        {
            Death();
        }
    }

    void UpdateHealthDisplay()
    {
        enemyHealthDisplay.text = "Enemy Health: " + health;

        if (health > 190)
        {
            ShowFullHearts(20);
        }
        else if (health > 180)
        {
            ShowFullHearts(19);
        }
        else if (health > 170)
        {
            ShowFullHearts(18);
        }
        else if (health > 160)
        {
            ShowFullHearts(17);
        }
        else if (health > 150)
        {
            ShowFullHearts(16);
        }
        else if (health > 140)
        {
            ShowFullHearts(15);
        }
        else if (health > 130)
        {
            ShowFullHearts(14);
        }
        else if (health > 120)
        {
            ShowFullHearts(13);
        }
        else if (health > 110)
        {
            ShowFullHearts(12);
        }
        else if (health > 100)
        {
            ShowFullHearts(11);
        }
        else if (health > 90)
        {
            ShowFullHearts(10);
        }
        else if (health > 80)
        {
            ShowFullHearts(9);
        }
        else if (health > 70)
        {
            ShowFullHearts(8);
        }
        else if (health > 60)
        {
            ShowFullHearts(7);
        }
        else if (health > 50)
        {
            ShowFullHearts(6);
        }
        else if (health > 40)
        {
            ShowFullHearts(5);
        }
        else if (health > 30)
        {
            ShowFullHearts(4);
        }
        else if (health > 20)
        {
            ShowFullHearts(3);
        }
        else if (health > 10)
        {
            ShowFullHearts(2);
        }
        else if (health > 0)
        {
            ShowFullHearts(1);
        }
        else
        {
            HideAllHearts();
        }
    }

    void ShowFullHearts(int count)
    {
        for (int i = 0; i < fullHeartImages.Count; i++)
        {
            if (i < count)
            {
                fullHeartImages[i].enabled = true;
            }
            else
            {
                fullHeartImages[i].enabled = false;
            }
        }
    }

    void HideAllHearts()
    {
        foreach (var image in fullHeartImages)
        {
            image.enabled = false;
        }
    }

    void Death()
    {
        Destroy(gameObject);
        scenes.enemeyDeath();
    }
}

