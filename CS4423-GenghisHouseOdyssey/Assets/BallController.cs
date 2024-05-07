using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float kickForce = 500f; // The force to apply when kicked
    [SerializeField] private float torqueMultiplier = 10f; // Multiplier for torque

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Apply torque when colliding with other objects
        // Calculate torque based on collision force and apply it to the Rigidbody2D
        Vector2 collisionDirection = collision.relativeVelocity.normalized;
        float appliedTorque = collision.relativeVelocity.magnitude * torqueMultiplier;

        rb.AddTorque(appliedTorque); // Apply torque to spin the ball

        // Additional logic to determine when to kick or add force
        if (collision.gameObject.CompareTag("Player")) // Assuming the object with tag "Player" kicks the ball
        {
            Vector2 forceDirection = (collision.transform.position - transform.position).normalized;
            rb.AddForce(-forceDirection * kickForce); // Apply force in the opposite direction
        }
    }
}
