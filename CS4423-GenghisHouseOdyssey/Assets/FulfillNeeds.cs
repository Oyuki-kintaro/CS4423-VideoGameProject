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
            PlayerDog.IncreaseStamina();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDog.DecreaseStamina();
        }
    }

    void Update()
    {
        
    }
}
