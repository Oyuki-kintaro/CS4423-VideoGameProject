using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] Dog PlayerDog;
    [SerializeField] OptionsMenu OptionMenu;

    void Start()
    {
        //projectileThrower = playerCreature.GetComponent<ProjectileThrower>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;

        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            input.y += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            input.y -= 1;
        }*/

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            input.x += -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            input.x += 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDog.Bark();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerDog.Poop();
        }

        if (Input.GetKey(KeyCode.O))
        {
            OptionMenu.OpenOptions();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerDog.LayDown();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        PlayerDog.MoveDog(input);
    }
}
