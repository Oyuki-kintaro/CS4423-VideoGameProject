using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FulfillNeeds : MonoBehaviour
{
    [SerializeField] Dog PlayerDog;
    // Start is called before the first frame update
    private bool playerInside = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    void Update()
    {
        if (playerInside)
        {
            if (gameObject.name == "FoodBowl")
            {
                PlayerDog.Eat();
                Debug.Log("Eating");
            }
            if (gameObject.name == "DogBed")
            {
                PlayerDog.Rest();
                Debug.Log("Sleeping");
            }
            
            
        }
        
    }
}
