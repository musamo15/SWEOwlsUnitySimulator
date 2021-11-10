using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{

    public float scrollSpeed = 0f;

    public Camera zoomCam;



    private void Update()
    {
        zoomCam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    }



}