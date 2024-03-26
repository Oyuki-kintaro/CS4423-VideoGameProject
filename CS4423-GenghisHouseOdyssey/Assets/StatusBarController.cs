using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarController : MonoBehaviour
{
    [SerializeField] SpriteRenderer symbolImage;
    [SerializeField] string statusBarObjectName;
    private Image statusBarImage;

    void Start()
    {
        // Find the child object with the specified name
        Transform statusBarObject = transform.Find(statusBarObjectName);
        
        // Get the Image component from the child object
        if (statusBarObject != null)
        {
            statusBarImage = statusBarObject.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("Child object with name " + statusBarObjectName + " not found!");
        }
    }

    void Update()
    {
        if (statusBarImage == null)
        {
            return; // Exit the method if statusBarImage is not assigned
        }
        
        // Calculate fill percentage
        float fillPercentage = statusBarImage.fillAmount * 100f;

        // Change color of symbol based on fill percentage
        if (fillPercentage >= 85f)
        {
            symbolImage.color = Color.green;
            statusBarImage.color = Color.green;
        }
        else if (fillPercentage <= 30f)
        {
            symbolImage.color = Color.red;
            statusBarImage.color = Color.red;
        }
        else
        {
            symbolImage.color = Color.yellow;
            statusBarImage.color = Color.yellow;
        }
    }
}
