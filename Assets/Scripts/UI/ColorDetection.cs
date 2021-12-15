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
    public ArrayList colorList = new ArrayList();



    void Start()
    {
        colorList.Add("Red");
        colorList.Add("Black");
        colorList.Add("Pink");
        colorList.Add("Yellow");
        colorList.Add("Green");
        colorList.Add("Light Green");
        colorList.Add("Blue");
        colorList.Add("Light Blue");
        colorList.Add("Violet");
        colorList.Add("Orange");
        colorList.Add("White");
    }

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
            
        if(colorList.Contains(currentColor))
        {
            colorTxt.text = currentColor;
        }
        else
        {
            currentColor = "None";
            colorTxt.text = currentColor;
        }

            
      



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

        if (colorList.Contains(currentColor))
        {
            colorTxt.text = currentColor;
        }
        else
        {
            currentColor = "None";
            colorTxt.text = currentColor;
        }

    }







}