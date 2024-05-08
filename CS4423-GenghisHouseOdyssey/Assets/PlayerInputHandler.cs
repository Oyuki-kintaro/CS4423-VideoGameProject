using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] Dog PlayerDog;
    [SerializeField] OptionsMenu OptionMenu;
    [SerializeField] private ScreenFader screenFader;

    void Start()
    {
        //projectileThrower = playerCreature.GetComponent<ProjectileThrower>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;

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

        /*if (Input.GetKey(KeyCode.O))
        {
            OptionMenu.OpenOptions();
        }*/

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerDog.LayDown();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            screenFader.FadeToColor("MainMenu");
        }
        PlayerDog.MoveDog(input);
    }
}
