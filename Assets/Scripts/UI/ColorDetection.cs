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
        detectedColor = other.name.ToString();


        if (detectedColor.Contains("Red"))
        {
            currentColor = "Red";
        }

        else if (detectedColor.Contains("Black"))
        {
            currentColor = "Black";
        }

        else if (detectedColor.Contains("White"))
        {
            currentColor = "White";
        }

        else if (detectedColor.Contains("Blue"))
           {

                if (detectedColor.Contains("Light"))
                {
                    currentColor = "Light Blue";
                }
                else
                {
                    currentColor = "Blue";
                }
                              
           }

        else if (detectedColor.Contains("Green"))
           {

                if (detectedColor.Contains("Light"))
                {
                    currentColor = "Light Green";
                
                }
                else
                {
                    currentColor = "Green";
                }

           }

        else if (detectedColor.Contains("Orange"))
           {
               currentColor = "Orange";
           }

        else if (detectedColor.Contains("Pink"))
           {
               currentColor = "Pink";   
           }

        else if (detectedColor.Contains("Violet"))
           {
               currentColor = "Violet"; 
           }

        else if (detectedColor.Contains("Yellow"))
           {
               currentColor = "Yellow";

           }


        colorTxt.text = currentColor;
    }
   
    void OnTriggerExit(Collider other)
    {

        currentColor = "None";
        colorTxt.text = currentColor;
    }

    void OnTriggerStay(Collider other)
    {
       detectedColor = other.name.ToString();


        if (detectedColor.Contains("Black"))
        {
            currentColor = "Black";

        }

        else if (detectedColor.Contains("Red"))
        {
            currentColor = "Red";

        }

        else if(detectedColor.Contains("White"))
        {
            currentColor = "White";

        }

        else if (detectedColor.Contains("Blue"))
        {

            if(detectedColor.Contains("Light"))
            {
                currentColor = "Light Blue";
            }
            else
            {
                currentColor = "Blue";
            }

        }

        else if (detectedColor.Contains("Green"))
        {
            
            if(detectedColor.Contains("Light"))
            {
                currentColor = "Light Green";
            }
            else
            {
                currentColor = "Green";
            }

        }

        else if (detectedColor.Contains("Orange"))
        {
            currentColor = "Orange";
        }

        else if (detectedColor.Contains("Pink"))
        {
            currentColor = "Pink";
        }

        else if (detectedColor.Contains("Violet"))
        {
            currentColor = "Violet";
        }

        else if (detectedColor.Contains("Yellow"))
        {
            currentColor = "Yellow";
        }


        colorTxt.text = currentColor;
    }




}