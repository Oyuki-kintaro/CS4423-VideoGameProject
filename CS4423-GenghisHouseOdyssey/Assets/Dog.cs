using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dog : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health = 3;
    [SerializeField] float speed = 0f;
    [SerializeField] float jumpForce = 10;
    [SerializeField] int stamina = 3;

    public enum CreatureMovementType { tf, physics };
    [SerializeField] CreatureMovementType movementType = CreatureMovementType.tf;

    [Header("Physics")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpOffset = -.5f;
    [SerializeField] float jumpRadius = .25f;

    [Header("Flavor")]
    [SerializeField] string creatureName = "Meepis";
    [SerializeField] private GameObject body;
    //[SerializeField] private List<AnimationStateChanger> animationStateChangers;

    [Header("Tracked Data")]
    [SerializeField] Vector3 homePosition = Vector3.zero;

    Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(health);

    }



    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate(){

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
    }

}
