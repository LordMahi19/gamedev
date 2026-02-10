
using UnityEngine;

public class brownCubeClockWise : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
