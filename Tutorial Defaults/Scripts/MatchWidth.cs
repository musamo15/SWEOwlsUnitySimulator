using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraAspectRatioAdjuster : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float aspectRatio = Screen.width / Screen.height;
        //you will need to change these numbers
        if (aspectRatio > (16 / 9f)) {
            camera.fieldOfView = 60;
        }
        else if (aspectRatio > 4 / 3f)
        {
            camera.fieldOfView = 55;
        }
        else
        {
            camera.fieldOfView = 50;
        }
        //alternatively, you could try a one-size-fits-all-formula
        //camera.fieldOfView = aspectRatio * 60;
    }
}