using System.Collections;
using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField] private ParticleSystem windParticleSystem; // Reference to the particle system
    [SerializeField] private float xMin = -10f; // Minimum x-value for random movement
    [SerializeField] private float xMax = 10f; // Maximum x-value for random movement
    [SerializeField] private float interval = 15f; // Time interval between activations

    private void Start()
    {
        StartCoroutine(WindRoutine()); // Start the coroutine for controlling the wind
    }

    private IEnumerator WindRoutine()
    {
        System.Random random = new System.Random(); // Random number generator

        while (true) // Continuous loop to keep the coroutine running
        {
            // Move to a random x position within the specified range
            float randomX = Random.Range(xMin, xMax);
            Vector3 newPosition = new Vector3(randomX, transform.position.y, transform.position.z);
            transform.position = newPosition; // Set the new position

            // Play the particle system
            windParticleSystem.Play();

            // Wait for a specified interval (15 seconds)
            yield return new WaitForSeconds(interval);

            // Stop the particle system
            windParticleSystem.Stop();
        }
    }
}
