using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseManager : MonoBehaviour
{
    public int phase;
    public Animator background;
    public Animator enemyAnimator;
    public SpriteRenderer leftBarrierRenderer;
    public SpriteRenderer rightBarrierRenderer;
    public SpriteRenderer floorRenderer;
    public SpriteRenderer floorForwardRenderer;
    public Sprite leftBarrierP1;
    public Sprite rightBarrierP1;
    public Sprite leftBarrierP2;
    public Sprite rightBarrierP2;
    public Sprite leftBarrierP3;
    public Sprite rightBarrierP3;
    public Sprite floorP1;
    public Sprite floorP2;
    public Sprite floorP3;
    public GameObject Floor;
    public GameObject bg;
    public GameObject FloorForward;
    public GameObject starryBG;



    public void phase1()
    {
        Floor.transform.localScale = new Vector3(10, 0.8f, 10);
        background.SetTrigger("P1");
        Debug.Log("Background Phase = P1");
        leftBarrierRenderer.sprite = leftBarrierP1;
        rightBarrierRenderer.sprite = rightBarrierP1;
        floorRenderer.sprite = floorP1;
        phase = 1;
    }

    // Update is called once per frame
    void Update()
    { 
    }
    public void phase2Transition()
    {
        background.SetTrigger("P2Transition");
        Debug.Log("Background Phase = P2");
        floorRenderer.sprite = floorP2;
        Floor.transform.localPosition = new Vector3(3f, -6.1f, -8.6f);
        Floor.transform.localRotation = Quaternion.Euler(90, 0, -180);
        Floor.transform.localScale = new Vector3(1.661f, 0.06f, 15f);
        leftBarrierRenderer.enabled = false;
        rightBarrierRenderer.enabled = false;
        phase = 2;
    }
    public void phase2()
    {
        background.SetTrigger("P2");
    }
    public void phase3Transition()
    {
        leftBarrierRenderer.enabled = false;
        rightBarrierRenderer.enabled = false;
        starryBG.SetActive(true);
        background.SetTrigger("P3");
        Debug.Log("Background Phase = P3Transition");
        floorRenderer.enabled = false;
        FloorForward.SetActive(false);


    }
    public void phase3()
    {
        background.SetTrigger("P3");
        phase = 3;
    }
}
