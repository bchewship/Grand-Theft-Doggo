using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEnemy : MonoBehaviour
{
    public float speed;
    private float detectionRange = 20.0f;
    public float rotationSpeed = 60.0f;
    
    private Rigidbody enemyRb;
    private GameObject player;

    private Animator enemyAnimation;

    private Renderer m_renderer;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemyAnimation = GetComponent<Animator>();
        m_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        

        //makes the enemy walk towards player if within range
        if (Vector3.Distance(player.transform.position, transform.position) <= detectionRange)
        {
            StopCoroutine(MovementRandom());
            enemyRb.transform.LookAt(player.transform.position);
            enemyRb.transform.Translate(lookDirection * speed * Time.deltaTime);
            enemyAnimation.SetFloat("Speed_f", 0.7f);
        }
        else if((Vector3.Distance(player.transform.position, transform.position) >= detectionRange))
        {
            enemyAnimation.SetFloat("Speed_f", 0);
            StartCoroutine(MovementRandom());
            
        }
    }

    IEnumerator MovementRandom()
    { 
        //enemy should walk in random direction after 5 seconds of idle -- right now only moves in circles
        yield return new WaitForSeconds(5);

        float randomRange = Random.Range(0.0f, 1.0f);
        Vector3 randomDirection = new Vector3(0, randomRange, 0);
        enemyRb.transform.Rotate(randomDirection);

        enemyRb.transform.position += transform.forward * speed * Time.deltaTime * 0.3f;
        enemyAnimation.SetFloat("Speed_f", 0.3f);
    }

}
