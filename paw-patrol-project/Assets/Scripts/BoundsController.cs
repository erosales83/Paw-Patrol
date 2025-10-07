using UnityEngine;

public class BoundsController : MonoBehaviour
{
    public float minX = -18f;
    public float maxX = 18f;
    public float minZ = -18f;
    public float maxZ = -1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 boundPosition = transform.position;

        if (boundPosition.x < minX) boundPosition.x = minX;
        if (boundPosition.x > maxX) boundPosition.x = maxX;
        if (boundPosition.z < minZ) boundPosition.z = minZ;
        if (boundPosition.z > maxZ) boundPosition.z = maxZ;

        transform.position = boundPosition;
    }
}
