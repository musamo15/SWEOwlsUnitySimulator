using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CameraToggle : MonoBehaviour
{
    public Camera ThreeD;
    public Camera ThreeD2;
    public Camera TwoD;
    public Button yourButton;
    public Canvas ui; //Need this for making the canvas fit the screen for both cameras

    public LevelEditorManager lem; //adding for robot selection fix

    public void Start()
    {
            Button btn = yourButton.GetComponent<Button>();
            btn.onClick.AddListener(FlipCams);
            ThreeD.enabled = false;
            ThreeD2.enabled = false;
            TwoD.enabled = true;
            ui.worldCamera = TwoD;
    }
    public void FlipCams()
    {
       
        ThreeD.enabled = !ThreeD.enabled;
        ThreeD2.enabled = !ThreeD2.enabled;
        TwoD.enabled = !TwoD.enabled;

        if (lem.ItemPrefabs[1].activeSelf == true) //SpikePrime index
        {
            if (ui.worldCamera == TwoD)
            {
                ui.worldCamera = ThreeD;
            }
            else
            {
                ui.worldCamera = TwoD;
            }
        }

        if(lem.ItemPrefabs[13].activeSelf == true) //SpikePrimeSideColor index
        {
            if (ui.worldCamera == TwoD)
            {
                ui.worldCamera = ThreeD2;
            }
            else
            {
                ui.worldCamera = TwoD;
            }
        }


    }
}
