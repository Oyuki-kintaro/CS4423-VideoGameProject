using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance; // Singleton instance

    [SerializeField] private AudioSource backgroundMusic; // Reference to the AudioSource for background music

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the singleton instance
            DontDestroyOnLoad(gameObject); // Ensure this object persists across scene changes

            if (backgroundMusic != null)
            {
                backgroundMusic.loop = true; // Ensure music loops
                backgroundMusic.Play(); // Start playing the background music
            }
        }
        else
        {
            // If an instance already exists, destroy this object to avoid duplicates
            Destroy(gameObject);
        }
    }
}
