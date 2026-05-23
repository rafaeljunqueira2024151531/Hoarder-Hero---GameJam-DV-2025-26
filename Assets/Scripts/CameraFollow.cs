using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float minX; 
    public float maxX; 
    private float fixedY;

    void Start() {
        fixedY = transform.position.y;
    }

    void LateUpdate() {
        if (target == null) return;

        float targetX = target.position.x + offset.x;
        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 desiredPosition = new Vector3(clampedX, fixedY, offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}