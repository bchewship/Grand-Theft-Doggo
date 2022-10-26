using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject cameraTarget;

    static float baseSpeed = 6.0f;
    static float multiplySpeed = 1.0f;
    static float playerSpeed = baseSpeed * multiplySpeed;
    public float rotationSpeed = 180;

    //affects ....jump force and gravity
    public float jumpForce = 5.0f;
    public float gravityModifier = 1.0f;
    
    //on ground
    private bool isOnGround = true;

    //animations
    private Animator playerAnimation;


    //particles

    //sounds
    public AudioClip[] barkSounds;

    //make the player an audio source
    private AudioSource playerAudio;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        cameraTarget = GameObject.Find("CameraTarget");
        //modifies gravity
        Physics.gravity *= gravityModifier;
        //get animator/sounds
        playerAnimation = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Bark();
        Move();

    }

    public void Move()
    {
        //gets inputs for horizontal and vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //moves in direction character is facing
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * verticalInput);

        //plays animation when moving
        //playerAnimation.SetFloat("Speed_f", verticalInput);

        //rotates character on left/right
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
        

        //can only jump while on ground
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            //jump animation and sound goes here
        }

        //when holding down arrow / S, backup slower than forward movement speed
        if(verticalInput <= 0)
        {
            playerSpeed = 1.5f;
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * verticalInput);
            playerAnimation.SetFloat("Speed_f", verticalInput * -0.5f);
        }
        //hold shift and w to sprint
        else if(Input.GetKey(KeyCode.LeftShift) && verticalInput > 0)
        {
            playerSpeed = 13.0f;
            playerAnimation.SetFloat("Speed_f", verticalInput);
        }
        else
        {
            playerSpeed = 6.0f;
            playerAnimation.SetFloat("Speed_f", verticalInput *0.7f);
        }

    }

    //plays a random bark on left mouse click
    private void Bark()
    {
        bool bark = Input.GetKeyDown(KeyCode.Mouse0);
        if (bark)
        {
            int index = Random.Range(0, barkSounds.Length);
            playerAudio.PlayOneShot(barkSounds[index]);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {

        }
    }
}
