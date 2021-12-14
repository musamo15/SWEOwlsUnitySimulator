using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorDetection : MonoBehaviour
{


    public string currentColor = "None";
    public Text colorTxt;
    public string colorID;
    public string detectedColor;
    


    void Awake()
    {
        currentColor = "None";
        colorTxt.text = currentColor;  
    }

    public string getCurrentColor()
    {
        return currentColor;
    }


    void OnTriggerEnter(Collider other)
    {
        currentColor = other.name.ToString().Replace("(Clone)", "");
        currentColor = currentColor.Replace("Image", "");

        colorTxt.text = currentColor;

    }
   
    void OnTriggerExit(Collider other)
    {
        currentColor = "None";
        colorTxt.text = currentColor;
    }

    void OnTriggerStay(Collider other)
    {
        currentColor = other.name.ToString().Replace("(Clone)", "");
        currentColor = currentColor.Replace("Image", "");

        colorTxt.text = currentColor;
    }







}