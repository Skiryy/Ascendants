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


    void Start()
    {
        victoryScreen.SetActive(false);
        StartCoroutine(startScene());
    }


    IEnumerator startScene()
    {
        Time.timeScale = 0;
        three.SetActive(true);
        two.SetActive(false);
        one.SetActive(false);
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
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(3);
        victoryScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene(sceneIndex + 1);
        victoryScreen.SetActive(false);

    }
}