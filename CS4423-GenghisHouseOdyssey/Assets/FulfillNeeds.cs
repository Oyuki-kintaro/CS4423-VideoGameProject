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
        
        if (gameObject.name == "FoodBowl" && other.CompareTag("Player"))
        {
            PlayerDog.Eat(); 
        }
        else if (gameObject.name == "DogBed" && other.CompareTag("Player"))
        {
            PlayerDog.IncreaseStamina();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.name == "FoodBowl" && other.CompareTag("Player"))
        {
            PlayerDog.StopEat(); 
        }
        else if (gameObject.name == "DogBed" && other.CompareTag("Player"))
        {
            PlayerDog.DecreaseStamina();
        }
    }
}
