using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public float speed = 8.0f;
    private float detectionRange = 25.0f;

    private Rigidbody rabbitRb;
    private GameObject player;

    private Animator rabbitAnimator;
    private Renderer m_renderer;

    void Start()
    {
        rabbitRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        rabbitAnimator = GetComponent<Animator>();
        m_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (transform.position - player.transform.position).normalized;
        //supposed to make rabbit flee from player
        if (Vector3.Distance(player.transform.position, transform.position) <= detectionRange)
        {
            StopCoroutine(MovementRandom());
            rabbitRb.transform.LookAt(player.transform.position);
            rabbitRb.transform.Rotate(0, 180, 0);
            rabbitRb.transform.Translate(lookDirection * speed * Time.deltaTime);
            
        }
        else if ((Vector3.Distance(player.transform.position, transform.position) >= detectionRange))
        {
            
            StartCoroutine(MovementRandom());

        }
    }

    IEnumerator MovementRandom()
    {
        //rabbit should walk in random direction after 5 seconds of idle -- right now only moves in circles
        yield return new WaitForSeconds(5);

        float randomRange = Random.Range(0.0f, 1.0f);
        Vector3 randomDirection = new Vector3(0, randomRange, 0);
        rabbitRb.transform.Rotate(randomDirection);

        rabbitRb.transform.position += transform.forward * speed * Time.deltaTime * 0.3f;
        
    }
}
