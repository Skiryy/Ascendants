using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenuScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Image intro1;
    public Image intro2;
    public Image intro3;
    public GameObject mainmenu;

    void Start()
    {
        StartCoroutine(introScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator introScene()
    {
        Debug.Log("wassgood");
        mainmenu.SetActive(false);
        intro1.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        intro2.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        intro3.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        mainmenu.SetActive(true);
        intro1.gameObject.SetActive(false);
        intro2.gameObject.SetActive(false);
        intro3.gameObject.SetActive(false);

    }
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingsButton()
    {

    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
