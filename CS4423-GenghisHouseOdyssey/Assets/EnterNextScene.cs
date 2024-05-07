using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNextScene : MonoBehaviour
{
    
    private Scene currentScene;
    // Start is called before the first frame update   
    [SerializeField] private List<AnimationStateChanger> DoorAnimationStateChangers; 
    private float doorAnimationDuration = 0.5f;
    private float playerPositionY = -4.06f;
    [SerializeField] private ScreenFader screenFader;
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
                Vector3 playerPosition = player.transform.position;
                Vector3 playerScale = player.transform.localScale;

                if(currentScene.name == "BackYard")
                {
                    playerPosition = new Vector3(doorX, playerPositionY, 0f);
                    playerScale.x = -Mathf.Abs(playerScale.x);
                }
                if(currentScene.name == "House")
                {
                    if(prevScene == "BackYard")
                    {
                        playerPosition = new Vector3(doorX, playerPositionY, 0f);
                        playerScale.x = Mathf.Abs(playerScale.x);
                    }
                    if(prevScene == "FrontYard")
                    {
                        playerPosition = new Vector3(doorX - 2.5f, playerPositionY, 0f);
                        playerScale.x = -Mathf.Abs(playerScale.x);
                    }
                    
                }
                if(currentScene.name == "FrontYard")
                {
                    playerPosition = new Vector3(doorX + 2.5f, playerPositionY, 0f);
                    playerScale.x = Mathf.Abs(playerScale.x);
                }

                player.transform.position = playerPosition;
                player.transform.localScale = playerScale;
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
                StartCoroutine(OpenDoorThenChangeScene("BackYard"));
            }

            if (other.CompareTag("Player") && this.gameObject.name == "FrontDoor") //&& Input.GetKey(KeyCode.Return))
            {
                // Load the specified scene
                StartCoroutine(OpenDoorThenChangeScene("FrontYard"));
            }
        }

        if(scene == "BackYard")
        {
            if (other.CompareTag("Player")) //&& Input.GetKey(KeyCode.Return))
            {
                // Load the specified scene
                screenFader.FadeToColor("House");
            }
        }

        if(scene == "FrontYard")
        {
            if (other.CompareTag("Player")) //&& Input.GetKey(KeyCode.Return))
            {
                // Load the specified scenev
                screenFader.FadeToColor("House");
            }
        }
        
    }

    private IEnumerator OpenDoorThenChangeScene(string sceneName)
    {
        // Trigger the door animation
        if (DoorAnimationStateChangers != null && DoorAnimationStateChangers.Count > 0)
        {
            foreach(AnimationStateChanger asc in DoorAnimationStateChangers){
                asc.ChangeAnimationState("DoorMotion");
            }
        }

        // Wait for the duration of the animation
        yield return new WaitForSeconds(doorAnimationDuration);

        // Load the next scene after the animation completes
        screenFader.FadeToColor(sceneName);
    }
}
