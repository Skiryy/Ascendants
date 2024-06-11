using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    bool escaped = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escaped)
            {
                Debug.Log("WHATS GOOD");
                Time.timeScale = 0;
                escaped = true;
            }
            else
            {
                Debug.Log("DAMN!");
                Time.timeScale = 1;
                escaped = false;
            }
        }
    }
}
