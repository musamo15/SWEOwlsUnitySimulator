using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CameraToggle : MonoBehaviour
{
    public Camera ThreeD;
    public Camera TwoD;
    public Button yourButton;
    public Canvas ui; //Need this for making the canvas fit the screen for both cameras

    public void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(FlipCams);
        ThreeD.enabled = false;
        TwoD.enabled = true;
        ui.worldCamera = TwoD;
    }
    public void FlipCams()
    {
       
        ThreeD.enabled = !ThreeD.enabled;
        TwoD.enabled = !TwoD.enabled;
        
        if(ui.worldCamera == TwoD)
        {
            ui.worldCamera = ThreeD;
        }
        else
        {
            ui.worldCamera = TwoD;
        }


    }
}
