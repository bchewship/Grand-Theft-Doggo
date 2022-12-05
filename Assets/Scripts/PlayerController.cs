using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject cameraTarget;
    private GameManager gameManager;

    static float baseSpeed = 6.0f;
    //will use multiplySpeed to change based on randomly generated players
    static float multiplySpeed = 1.0f;
    static float playerSpeed = baseSpeed * multiplySpeed;
    //speed of rotation
    public float rotationSpeed = 180;

    //affects jump force and gravity
    public float jumpForce = 5.0f;
    public float gravityModifier = 1.0f;
    //how far player is pushed back when hit by enemy -- higher = lighter
    public float lightness = 10.0f;
    
    //on ground
    private bool isOnGround = true;

    //animations/particles
    private Animator playerAnimation;
    public ParticleSystem damageParticle;

    //sounds
    public AudioClip[] barkSounds;

    //make the player an audio source
    private AudioSource playerAudio;

    public GameObject coin;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        cameraTarget = GameObject.Find("CameraTarget");
        //modifies gravity
        Physics.gravity *= gravityModifier;
        //get animator/sounds
        playerAnimation = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Bark();
        Move();
        Pause();
        //if player somehow gets underground, will place them back above ground
        if (playerRb.position.y < -3)
        {
            playerRb.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        }
    }

    public void Move()
    {
        //gets inputs for horizontal and vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontalInput, 0, verticalInput);

        //moves in direction character is facing
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * verticalInput);
        //playerRb.MovePosition(transform.position + moveInput * Time.deltaTime * playerSpeed);

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
            playerAnimation.SetFloat("Speed_f", verticalInput * 0.7f);
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
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushPlayer = (playerRb.transform.position - enemyRigidbody.transform.position);
            damageParticle.Play();
            //push player away from enemy
            playerRb.AddForce(pushPlayer * lightness, ForceMode.Impulse);
            gameManager.UpdateHealth(-1);
        }
        else if (collision.gameObject.CompareTag("Neutral"))
        {
            Rigidbody neutralRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushNeutral = neutralRb.transform.position - playerRb.transform.position;
            damageParticle.Play();
            neutralRb.AddForce(pushNeutral * lightness * 2, ForceMode.Impulse);
            //makes bunny throw a coin up when collided
            var dropCoin = Instantiate(coin, new Vector3(1,1,1) + neutralRb.transform.position, neutralRb.rotation);
            dropCoin.GetComponent<Rigidbody>().AddForce(new Vector3(0,5f,0) * 3, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("CoinPickup"))
        {
            //gives +1 coin and +1 health when picking up coin
            gameManager.UpdateCurrency(1);
            gameManager.UpdateHealth(1);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Upgrade"))
        {
            //code for upgrades goes here
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerRb.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        if (collision.gameObject.CompareTag("Building"))
        {
            playerRb.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseGame();
        }
    }
}
