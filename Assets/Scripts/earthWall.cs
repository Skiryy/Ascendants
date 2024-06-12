using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthWall : MonoBehaviour
{
    public int hitstaken = 0;
    public int totalHits = 5;
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void increaseHits()
    {
        hitstaken += 1;
        Debug.Log(hitstaken);
    }
    // Update is called once per frame
    void Update()
    {
        if (hitstaken >= totalHits)
        {
            Destroy(wall);
            hitstaken = 0;
        }
    }
}
