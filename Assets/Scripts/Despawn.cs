using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{

    private Renderer m_renderer;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartDespawn();
    }

    void StartDespawn()
    {
        bool inSight = false;

        //if not in sight 
        if (inSight == false && !m_renderer.isVisible)
        {
            StartCoroutine("TimeToDespawn");
            Debug.Log("bye");
        }

        //if the object is on screen, it won't be destoyed, and it won't be destroyed if it leaves the screen and comes back
        if (m_renderer.isVisible)
        {
            Debug.Log("hi");
            inSight = true;
            StopCoroutine("TimeToDespawn");
        }
    }

    IEnumerator TimeToDespawn()
    {
        //wait for 10 seconds before destroying object
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
