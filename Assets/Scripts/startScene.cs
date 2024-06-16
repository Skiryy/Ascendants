using UnityEngine;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    private bool isPlayerInStartZone = false;
    private bool isPlayerInTutorialZone = false;
    public GameObject fightSelector;
    public GameObject tutorial;

    void Start()
    {
        Cursor.visible = false;
        fightSelector.SetActive(false);
        tutorial.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInStartZone && Input.GetKeyDown(KeyCode.E))
        {
            StartFunction();
        }
        if (isPlayerInTutorialZone)
        {
            tutorialFunction();
        }
    }

    void StartFunction()
    {
        Debug.Log("Start function called!");
        fightSelector.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void tutorialFunction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            tutorial.SetActive(false); 
        }
        else
        {
            tutorial.SetActive(true); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Start"))
        {
            isPlayerInStartZone = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Tutorial"))
        {
            isPlayerInTutorialZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Start"))
        {
            isPlayerInStartZone = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Tutorial"))
        {
            isPlayerInTutorialZone = false;
            tutorial.SetActive(false); 
        }
    }

    public void tankFight()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Hell yeah");
    }
}
