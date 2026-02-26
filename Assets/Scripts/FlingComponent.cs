using UnityEngine;

public class FlingComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector3 GetRandomVector3(float min, float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearVelocity = GetRandomVector3(0.1f, 4.0f);
        rb.angularVelocity = GetRandomVector3(0.1f, 4.0f);
    }
}
