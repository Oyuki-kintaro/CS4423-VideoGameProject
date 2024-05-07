using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dog : MonoBehaviour
{
    
    [SerializeField] private ScreenFader screenFader;
    
    [Header("Stats")]
    [SerializeField] float barkCost = 4f;
    [SerializeField] float poopOffsetX = -0.5f;
    [SerializeField] float poopOffsetY = -0.5f;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject dogPoopPrefab;

    [Header("Player Movement")]
    [SerializeField] float speed = 5f;
    public enum CreatureMovementType { tf, physics };
    [SerializeField] CreatureMovementType movementType = CreatureMovementType.tf;
    [SerializeField] private List<AnimationStateChanger> DogAnimationStateChangers;


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
    private bool isLayingDown = false; 
    private bool recovery = false;
    private bool isEating = false;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        screenFader.FadeToClear();
        // Load the player's stats from PlayerPrefs
        LoadPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        bathroom -= bathroomCost * Time.deltaTime;
        if(bathroom < 0 ) bathroom = 0;
        BathroomBar.fillAmount = bathroom / maxBathroom;

        hunger -= hungerCost * Time.deltaTime;
        if(bathroom < 0 ) hunger = 0;
        HungerBar.fillAmount = hunger / maxHunger;

        if (isEating)
            Eat();
        
        if (recovery)
        {
            stamina += (recoveryRate * Time.deltaTime);
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            StaminaBar.fillAmount = stamina / maxStamina;
        }

        if(isLayingDown) return;

        if (stamina <= 10f && !isLayingDown) // Check if stamina is 10f or less
        {
            LayDown(); // Change to "LayDown" animation
        }
        else if (stamina >= 29f && isLayingDown) // Check if stamina is 30f or more
        {
            DogAnimationStateChangers[0].ChangeAnimationState("Up");
            isLayingDown = false;
            recovery = false;
        }

        if (bathroom <= 10f) 
        {
            Poop();
        }
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
        if(direction.x != 0){
            recovery = false;
            isLayingDown = false;
            foreach(AnimationStateChanger asc in DogAnimationStateChangers){
                asc.ChangeAnimationState("Walk");
            }
            
        }else if (!isLayingDown){
            recovery = false;
            isLayingDown = false;
            foreach(AnimationStateChanger asc in DogAnimationStateChangers){
                asc.ChangeAnimationState("Idle");
            }
        }
    }

    public void MoveCreatureRb(Vector3 direction)
    {
        Vector3 currentVelocity = Vector3.zero;//new Vector3(0, rb.velocity.y, 0);

        rb.velocity = (currentVelocity) + (direction * speed);
        if(rb.velocity.x < 0){
            body.transform.localScale = new Vector3(-1,1,1);
        }else if(rb.velocity.x > 0){
            body.transform.localScale = new Vector3(1,1,1);
        }
    }

    public void MoveCreatureTransform(Vector3 direction)
    {
        body.transform.localScale += direction * Time.deltaTime * speed;
    }

    public void Bark()
    {        
        DogAnimationStateChangers[0].ChangeAnimationState("Bark"); // Trigger "Bark"

        GetComponent<AudioSource>().Play(); // Play the bark sound

        // Decrease hunger and stamina
        hunger -= (hungerCost * barkCost) * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        HungerBar.fillAmount = hunger / maxHunger;

        stamina -= (runCost * barkCost) * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        StaminaBar.fillAmount = stamina / maxStamina;
    }

    public void IncreaseStamina()
    {
        recoveryRate = recoveryRate * 2f;
    }

    public void DecreaseStamina()
    {
        recoveryRate = recoveryRate / 2f;
    }

    public void LayDown(){
        DogAnimationStateChangers[0].ChangeAnimationState("LayDown");   
        recovery = true;     
        isLayingDown = true;
    }

    public void Eat()
    {
        isEating = true;
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

    public void StopEat() 
    {
        isEating = false;
    }

    public void Poop() 
    {
        if (bathroom < (maxBathroom - 5))
        {
            //Vector3 spawnPosition = body.transform.localScale + new Vector3(poopOffsetX, poopOffsetY, 0f);
            float offsetX = (body.transform.localScale.x > 0) ? poopOffsetX : -poopOffsetX;
            Vector3 spawnPosition = body.transform.position + new Vector3(offsetX, poopOffsetY, 0f);

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
