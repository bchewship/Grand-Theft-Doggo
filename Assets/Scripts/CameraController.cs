using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        //angle and initial height of the camera
        offset = new Vector3(25, 50, -20);
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the camera using the mouse
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
    }
}
