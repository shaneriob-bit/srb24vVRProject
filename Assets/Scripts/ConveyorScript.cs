using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    public float speed;
    public Vector3 direction;

    // Use OnTriggerStay or OnCollisionStay
    void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        Debug.Log("dek0");
        if (rb != null)
        {
            // Apply force to the object in the specified direction
            // Using AddForce works well with physics
            rb.AddForce(direction * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    /*
    // Optional: Visually move the conveyor belt texture (separate from the physics)
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && renderer.material.mainTexture != null)
        {
            Vector2 offset = renderer.material.mainTextureOffset;
            // Assuming the texture is oriented vertically along the movement axis
            offset.y += Time.deltaTime * speed / 10f; // Adjust divisor for suitable visual speed
            renderer.material.mainTextureOffset = offset;
        }
    }
    */
}
