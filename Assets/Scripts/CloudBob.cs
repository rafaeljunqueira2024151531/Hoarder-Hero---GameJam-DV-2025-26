using UnityEngine;

public class CloudBob : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float speed = 0.5f;
    public float offsetX = 0f;  // muda em cada nuvem para não sincronizarem

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin((Time.time + offsetX) * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}