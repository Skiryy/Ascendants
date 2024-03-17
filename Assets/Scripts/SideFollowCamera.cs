using UnityEngine;

public class SideFollowCamera : MonoBehaviour
{
    public Transform target; // The player's transform
    public float smoothTime = 0.3f; // Smoothing time for the camera movement
    public Vector3 offset = new Vector3(2f, 0f, -10f); // Offset from the player

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError("No target assigned for SideFollowCamera!");
            return;
        }

        // Calculate the target position with the offset
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
