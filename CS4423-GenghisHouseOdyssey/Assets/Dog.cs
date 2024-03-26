using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dog : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed = 0f;
    [SerializeField] float barkCost = 4f;
    [SerializeField] float poopOffsetX = -0.5f;
    [SerializeField] float poopOffsetY = -0.5f;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject dogPoopPrefab;

    public enum CreatureMovementType { tf, physics };
    [SerializeField] CreatureMovementType movementType = CreatureMovementType.tf;

    
    //[SerializeField] private List<AnimationStateChanger> animationStateChangers;

    [Header("Health Status")]
    [SerializeField]  Image StaminaBar;
    [SerializeField]  float stamina = 100;
    [SerializeField]  float maxStamina = 100;
    [SerializeField]  float runCost = 0.1f;
    [SerializeField] float recoveryRate = 5f;

    [SerializeField]  Image HungerBar;
    [SerializeField]  float hunger = 100; 
    [SerializeField]  float maxHunger = 100;
    [SerializeField]  float hungerCost = 1;

    [SerializeField]  Image BathroomBar;
    [SerializeField]  float bathroom = 100;
    [SerializeField]  float maxBathroom = 100;
    [SerializeField]  float bathroomCost = 0.01f;
    Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Load the player's stats from PlayerPrefs
        LoadPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        bathroom -= bathroomCost * Time.deltaTime;
        if(bathroom < 0 ) bathroom = 0;
        BathroomBar.fillAmount = bathroom / maxBathroom;
    }

    public void MoveDog(Vector3 direction)
    {

        if (movementType == CreatureMovementType.tf)
        {
            MoveCreatureTransform(direction);
        }
        else if (movementType == CreatureMovementType.physics)
        {
            MoveCreatureRb(direction);
        }

        stamina -= runCost * Time.deltaTime;
        if(stamina < 0 ) stamina = 0;
        StaminaBar.fillAmount = stamina / maxStamina;

        //set animation
        /*if(direction.x != 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Walk",speed);
            }
        }else{
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("Idle");
            }
        }*/

    }

    public void MoveCreatureRb(Vector3 direction)
    {
        Vector3 currentVelocity = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = (currentVelocity) + (direction * speed);
        if(rb.velocity.x < 0){
            body.transform.localScale = new Vector3(-1,1,1);
        }else if(rb.velocity.x > 0){
            body.transform.localScale = new Vector3(1,1,1);
        }
        //rb.AddForce(direction * speed);
        //rb.MovePosition(transform.position + (direction * speed * Time.deltaTime))
    }

    public void MoveCreatureTransform(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    public void Bark()
    {
        GetComponent<AudioSource>().Play();

        hunger -= (hungerCost * barkCost) * Time.deltaTime;
        if(hunger < 0 ) hunger = 0;
        HungerBar.fillAmount = hunger / maxHunger;

        stamina -= (runCost * barkCost) * Time.deltaTime;
        if(stamina < 0 ) stamina = 0;
        StaminaBar.fillAmount = stamina / maxStamina;
    }

    public void Rest()
    {
        stamina += (recoveryRate * Time.deltaTime);
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        StaminaBar.fillAmount = stamina / maxStamina;
    }

    public void Eat()
    {
        // fill hunger status bar
        hunger += (recoveryRate * Time.deltaTime);
        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        HungerBar.fillAmount = hunger / maxHunger;

        // deplete bathroom status bar
        if (hunger < maxHunger)
        {
            bathroom -= (bathroomCost * hungerCost) * Time.deltaTime;
            if(bathroom < 0 ) bathroom = 0;
            BathroomBar.fillAmount = bathroom / maxBathroom;
        }
        
    }

    public void Poop() 
    {
        if (bathroom < (maxBathroom - 5))
        {
            Vector3 spawnPosition = transform.position + new Vector3(poopOffsetX, poopOffsetY, 0f);
            Instantiate(dogPoopPrefab, spawnPosition, Quaternion.identity);

            bathroom = maxBathroom;
            BathroomBar.fillAmount = bathroom / maxBathroom;
        }
        
    }

    void OnDestroy()
    {
        // Save player's stats to PlayerPrefs when the game object is destroyed
        SavePlayerStats();
    }

    void SavePlayerStats()
    {
        // Save the player's stats to PlayerPrefs
        PlayerPrefs.SetFloat("Stamina", stamina);
        PlayerPrefs.SetFloat("Hunger", hunger);
        PlayerPrefs.SetFloat("Bathroom", bathroom);
    }

    void LoadPlayerStats()
    {
        // Load the player's stats from PlayerPrefs
        stamina = PlayerPrefs.GetFloat("Stamina", maxStamina);
        hunger = PlayerPrefs.GetFloat("Hunger", maxHunger);
        bathroom = PlayerPrefs.GetFloat("Bathroom", maxBathroom);

        StaminaBar.fillAmount = stamina / maxStamina;
        HungerBar.fillAmount = hunger / maxHunger;
        BathroomBar.fillAmount = bathroom / maxBathroom;
    }

}
