using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CameraToggle : MonoBehaviour
{
    public Camera ThreeD;
    public Camera TwoD;
    public Button yourButton;

    public void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(FlipCams);
        ThreeD.enabled = true;
        TwoD.enabled = false;
    }
    public void FlipCams()
    {
       
        ThreeD.enabled = !ThreeD.enabled;
        TwoD.enabled = !TwoD.enabled;
    }
}
