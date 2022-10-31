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

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemyAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        
        //makes the enemy walk towards player if within range
        if(Vector3.Distance(player.transform.position, transform.position) <= detectionRange)
        {
            detectionRange = detectionRange + 10.0f;
            StopCoroutine(MovementRandom());
            enemyRb.transform.LookAt(player.transform.position);
            enemyRb.transform.Translate(lookDirection * speed * Time.deltaTime);
            enemyAnimation.SetFloat("Speed_f", 0.7f);
        }
        else
        {
            detectionRange = 20.0f;
            enemyAnimation.SetFloat("Speed_f", 0);
            StartCoroutine(MovementRandom());
        }
    }

    IEnumerator MovementRandom()
    {
        //enemy should move in random direction after 5 seconds of idle -- right now only moves in circles
        yield return new WaitForSeconds(5);
        Vector3 randomDirection = new Vector3(0, Random.value, 0);
        transform.Rotate(randomDirection);
        transform.position += transform.forward * speed * Time.deltaTime * 0.3f;
        enemyAnimation.SetFloat("Speed_f", 0.3f);
    }
}
