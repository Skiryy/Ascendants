using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject victoryScreen;


    // Start is called before the first frame update
    void Start()
    {
        victoryScreen.SetActive(false);
        StartCoroutine(startScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator startScene()
    {
        Time.timeScale = 0;
        three.SetActive(true);
        two.SetActive(false);
        one.SetActive(false);
        //3 seconds image
        Debug.Log("3");
        yield return new WaitForSecondsRealtime(1);
        three.SetActive(false);
        two.SetActive(true);
        one.SetActive(false);
        Debug.Log("2");
        yield return new WaitForSecondsRealtime(1);
        three.SetActive(false);
        two.SetActive(false);
        one.SetActive(true);
        Debug.Log("1");
        yield return new WaitForSecondsRealtime(1);
        three.SetActive(false);
        two.SetActive(false);
        one.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("0");
        yield return null;
    }
    public void enemeyDeath()
    { 
        StartCoroutine(death());
    }
    IEnumerator death()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1);
        victoryScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(2);
        victoryScreen.SetActive(false);

    }
}