using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNextScene : MonoBehaviour
{
    
    private Scene currentScene;
    // Start is called before the first frame update    
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        // Check if the door position is stored in PlayerPrefs
        if (PlayerPrefs.HasKey("DoorX") && PlayerPrefs.HasKey("DoorY"))
        {
            // Get the door position from PlayerPrefs
            float doorX = PlayerPrefs.GetFloat("DoorX");
            float doorY = PlayerPrefs.GetFloat("DoorY");

            string prevScene = PlayerPrefs.GetString("Prev Scene");
            

            // Position the player in front of the door
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if(currentScene.name == "BackYard")
                {
                    player.transform.position = new Vector3(doorX, doorY + 1.3f, 0f); // Adjust Y position as needed
                }
                if(currentScene.name == "House")
                {
                    if(prevScene == "BackYard")
                    {
                        player.transform.position = new Vector3(doorX, doorY - 1.3f, 0f); // Adjust Y position as needed
                    }
                    if(prevScene == "FrontYard")
                    {
                        player.transform.position = new Vector3(doorX, doorY + 1.3f, 0f); // Adjust Y position as needed
                    }
                    
                }
                if(currentScene.name == "FrontYard")
                {
                    player.transform.position = new Vector3(doorX, doorY + 1.3f, 0f); // Adjust Y position as needed
                }
            }

            // Remove the stored door position from PlayerPrefs
            PlayerPrefs.DeleteKey("DoorX");
            PlayerPrefs.DeleteKey("DoorY");
            PlayerPrefs.DeleteKey("Prev Scene");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currentScene = SceneManager.GetActiveScene();

        Vector3 doorPosition = transform.position;
        // Save the door position to PlayerPrefs
        PlayerPrefs.SetFloat("DoorX", doorPosition.x);
        PlayerPrefs.SetFloat("DoorY", doorPosition.y); 

        string scene = currentScene.name;
        PlayerPrefs.SetString("Prev Scene", scene);
        
        // Check if the collider is the player
        if(scene == "House")
        {
            if (other.CompareTag("Player") && this.gameObject.name == "BackDoor") //&& Input.GetKey(KeyCode.Return))
            {
                // Load the specified scene
                SceneManager.LoadScene("BackYard");
            }

            if (other.CompareTag("Player") && this.gameObject.name == "FrontDoor") //&& Input.GetKey(KeyCode.Return))
            {
                // Load the specified scene
                SceneManager.LoadScene("FrontYard");
            }
        }

        if(scene == "BackYard")
        {
            if (other.CompareTag("Player")) //&& Input.GetKey(KeyCode.Return))
            {
                // Load the specified scene
                SceneManager.LoadScene("House");
            }
        }

        if(scene == "FrontYard")
        {
            if (other.CompareTag("Player")) //&& Input.GetKey(KeyCode.Return))
            {
                // Load the specified scene
                SceneManager.LoadScene("House");
            }
        }
        
    }
}
