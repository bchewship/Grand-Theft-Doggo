using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    public float sensitivity = 1.0f;
    public float minZoom = 2.0f;
    public float maxZoom = 0.5f;
    public float zoomSpeed = 30.0f;
    private float targetZoom;

    void Start()
    {
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //zooms in and out on mousewheel up/down
        if (Input.mouseScrollDelta.y == 0)
        {
            return;
        }
        targetZoom = cam.orthographicSize - Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(targetZoom, minZoom, maxZoom);
    }
}
