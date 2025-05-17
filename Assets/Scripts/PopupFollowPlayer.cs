using UnityEngine;

public class PopupFollowPlayer : MonoBehaviour
{
    public Transform playerHead;
    public float followDistance = 2.5f;
    public float verticalOffset = -0.3f;
    public float smoothTime = 0.5f;
    public float rotationSmoothSpeed = 10f;
    public float maxVerticalAngle = 45f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPos = playerHead.position + playerHead.forward * followDistance;
        targetPos.y += verticalOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        Vector3 toHead = playerHead.position - transform.position;
        Vector3 flatForward = new Vector3(toHead.x, 0f, toHead.z);
        Quaternion yawRotation = Quaternion.LookRotation(flatForward.normalized, Vector3.up);

        float verticalAngle = -Vector3.SignedAngle(flatForward.normalized, toHead.normalized, transform.right);
        verticalAngle = Mathf.Clamp(verticalAngle, -maxVerticalAngle, maxVerticalAngle);
        Quaternion pitchRotation = Quaternion.AngleAxis(verticalAngle, Vector3.right);

        Quaternion finalRotation = yawRotation * pitchRotation * Quaternion.Euler(0, 180f, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * rotationSmoothSpeed);
    }
}