using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dragonEnemyScript : MonoBehaviour
{
    public float health = 100f;
    public GameObject barrel;
    public TextMeshProUGUI enemyHealthDisplay;
    public List<Image> fullHeartImages; // List of Image components for the full hearts

    // Start is called before the first frame update
    void Start()
    {

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
        // Update the health text display
        enemyHealthDisplay.text = "Enemy Health: " + health;

        // Update the heart images based on the health value
        if (health >= 91)
        {
            ShowFullHearts(10);
        }
        else if (health >= 81)
        {
            ShowFullHearts(9);
        }
        else if (health >= 71)
        {
            ShowFullHearts(8);
        }
        else if (health >= 61)
        {
            ShowFullHearts(7);
        }
        else if (health >= 51)
        {
            ShowFullHearts(6);
        }
        else if (health >= 41)
        {
            ShowFullHearts(5);
        }
        else if (health >= 31)
        {
            ShowFullHearts(4);
        }
        else if (health >= 21)
        {
            ShowFullHearts(3);
        }
        else if (health >= 11)
        {
            ShowFullHearts(2);
        }
        else if (health >= 1)
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
    }
}
