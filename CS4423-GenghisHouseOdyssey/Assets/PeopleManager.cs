using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PeopleManager : MonoBehaviour
{
    [SerializeField] private GameObject oldManPrefab;
    [SerializeField] private GameObject manPrefab; // Prefab for man
    [SerializeField] private GameObject womanPrefab; // Prefab for woman
    [SerializeField] private Transform peopleParent; // Parent transform to organize objects
    [SerializeField] private int initialPoolSize = 10; // Initial size of the object pool
    [SerializeField] private float yObjectPosition; // Initial size of the object pool
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float spawnInterval = 60f;

    private List<ObjectPool<GameObject>> peoplePools; // The object pool for managing people sprites
    private bool isSpriteActive = false;

    private void Start()
    {
        // Create the object pool
        peoplePools = new List<ObjectPool<GameObject>>
        {
            new ObjectPool<GameObject>(manPrefab, initialPoolSize, peopleParent),
            new ObjectPool<GameObject>(womanPrefab, initialPoolSize, peopleParent),
            new ObjectPool<GameObject>(oldManPrefab, initialPoolSize, peopleParent)
        };
        StartCoroutine(SpawnSpriteAtIntervals());
    }

    private IEnumerator SpawnSpriteAtIntervals()
    {
        while (true)
        {
            // Wait for the spawn interval before generating the next sprite
            yield return new WaitForSeconds(spawnInterval);

            if (!isSpriteActive) // Only spawn a new sprite if none is active
            {
                SpawnPerson();
            }
        }
    }

    private void SpawnPerson()
    {
        int randomIndex = Random.Range(0, peoplePools.Count); // Randomly choose man or woman
        ObjectPool<GameObject> selectedPool = peoplePools[randomIndex]; // Select the random pool
        
        GameObject person = selectedPool.Get(); // Get a person from the selected pool

        // Set the position and start moving across the scene
        person.transform.position = new Vector3(xMin, yObjectPosition, 0f);

        // Set to move across the scene from minX to maxX
        StartCoroutine(MovePersonAcrossScene(person, selectedPool));

        isSpriteActive = true; // Mark that a sprite is active
    }

    private IEnumerator MovePersonAcrossScene(GameObject person, ObjectPool<GameObject> selectedPool)
    {
        float speed = 1f; // Set a suitable speed for movement

        while (person.transform.position.x < xMax)
        {
            person.transform.Translate(Vector3.right * speed * Time.deltaTime); // Move towards xMax
            yield return null; // Continue to the next frame
        }

        // When the sprite reaches maxX, return it to the pool
        ReturnPerson(person, selectedPool);
    }

    // Example method to return a person to the pool
    public void ReturnPerson(GameObject person, ObjectPool<GameObject> selectedPool)
    {
        selectedPool.Return(person);
        isSpriteActive = false; // Mark that no sprite is active
    }
}
