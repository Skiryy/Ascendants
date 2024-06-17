using System.Collections;
using UnityEngine;

public class PlayerWaterAttacks : MonoBehaviour
{
    private CharacterMover characterMover; 
    public GameObject icicle;
    public bool attackStatus = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        characterMover = GetComponent<CharacterMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attackStatus)
        {
            bool rotateValue = characterMover.characterRotate;
            StartCoroutine(PerformAttack());        }
    }

    IEnumerator PerformAttack()
    {
        attackStatus = true;

        bool attackDirection = characterMover.characterRotate;
        Vector3 playerPosition = transform.position;
        int direction = (attackDirection) ? 1 : -1;
        Vector3 offsetPosition = playerPosition + Vector3.right * 01f * direction;

        if (attackDirection == false)
        {
            GameObject icicleAttadckInstance = Instantiate(icicle, offsetPosition, Quaternion.Euler(0, 270, Random.Range(0f, 360f)));
            //FireAttackInstance.transform.Translate(Vector3.left * direction);
            Destroy(icicleAttadckInstance, 2f);
        }
        else
        {
            GameObject icicleAttadckInstance = Instantiate(icicle, offsetPosition, Quaternion.Euler(0, 90, Random.Range(0f, 360f)));
            //FireAttackInstance.transform.Translate(Vector3.left * direction);
            Destroy(icicleAttadckInstance, 2f);
        }
        yield return new WaitForSeconds(0.2f);
        attackStatus = false;
    }
}
