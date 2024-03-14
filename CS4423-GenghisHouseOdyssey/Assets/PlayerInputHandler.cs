using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] Dog PlayerDog;

    void Start()
    {
        //projectileThrower = playerCreature.GetComponent<ProjectileThrower>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            input.y += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            input.y -= 1;
        }

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

        PlayerDog.MoveDog(input);

    }
}
