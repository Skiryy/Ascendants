using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    bool escaped = false;
    public GameObject escMenu;
    public phaseManager PhaseManager;
    private void Start()
    {
        escMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escaped)
            {
                Debug.Log("WHATS GOOD");
                Time.timeScale = 0;
                escaped = true;
                escMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                if (PhaseManager.phase == 3)
                {
                    Debug.Log("DAMN!");
                    Time.timeScale = 0.5f;
                    escaped = false;
                    escMenu.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Debug.Log("DAMN!");
                    Time.timeScale = 1;
                    escaped = false;
                    escMenu.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        //open settings
    }
}
