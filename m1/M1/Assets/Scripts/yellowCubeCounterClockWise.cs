
using UnityEngine;

public class yellowCubeCounterClockWise : MonoBehaviour
{
    public float rotationSpeed = 220f;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
