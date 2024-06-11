using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class startScene : MonoBehaviour
{
    private bool isPlayerInStartZone = false;
    public GameObject fightSelector;
    
    void Start()
    {
        Cursor.visible = false;

        fightSelector.SetActive(false); 
    }
    // Update is called once per frame
    void Update()
    {

        if (isPlayerInStartZone && Input.GetKeyDown(KeyCode.E))
        {
            StartFunction();
        }
    }


    void StartFunction()
    {
        // Call the desired function
        Debug.Log("Start function called!");
        fightSelector.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // This function will be called when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is on the "Start" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Start"))
        {
            isPlayerInStartZone = true;
        }
    }

    // This function will be called when the player exits a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the collider is on the "Start" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Start"))
        {
            isPlayerInStartZone = false;
        }
    }
    public void tankFight()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Hell yeah");
    }



}


