using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalSceneScript : MonoBehaviour
{
    private bool isPlayerInStartZone = false;
    public GameObject finalImage;

    void Start()
    {
        finalImage.SetActive(false);
    }

    void Update()
    {
        Debug.Log("test");
        if (isPlayerInStartZone)
        {
            Debug.Log("Hey");
            winFunction();
        }
    }
    void winFunction()
    {
        StartCoroutine(finale());
    }
    IEnumerator finale()
    {
        finalImage.SetActive(true);
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            isPlayerInStartZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Debug.Log("Hey");
            isPlayerInStartZone = false;
        }
    }
}
