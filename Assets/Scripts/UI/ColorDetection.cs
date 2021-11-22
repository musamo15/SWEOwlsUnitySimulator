using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorDetection : MonoBehaviour
{


    public string currentColor = "None";
    public Text colorTxt;
    public string colorID;
    


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

       
        
        if (other.name.ToString().Contains("Red"))
        {
            currentColor = "Red";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Black"))
        {
            currentColor = "Black";
            colorTxt.text = currentColor;

        }

        if ((other.name.ToString()).Contains("White"))
        {
            currentColor = "White";
            colorTxt.text = currentColor;

        }
              if ((other.name.ToString()).Contains("Blue("))
                {
                    currentColor = "Blue";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Green("))
                {
                    currentColor = "Green";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Light Green("))
                {
                    currentColor = "Light Green";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Orange"))
                {
                    currentColor = "Orange";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Pink"))
                {
                    currentColor = "Pink";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Violet"))
                {
                    currentColor = "Violet";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Yellow"))
                {
                    currentColor = "Yellow";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Light Blue("))
                {
                    currentColor = "Light Blue";
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
       
        if (other.name.ToString().Contains("Black"))
        {
            currentColor = "Black";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Red"))
        {
            currentColor = "Red";
            colorTxt.text = currentColor;

        }

        if ((other.name.ToString()).Contains("White"))
        {
            currentColor = "White";
            
            colorTxt.text = currentColor;

        }
        if ((other.name.ToString()).Contains("Blue("))
        {
       
            currentColor = "Blue";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Green("))
        {
            currentColor = "Green";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Light Green("))
        {
            
            currentColor = "Light Green";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Orange"))
        {
            currentColor = "Orange";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Pink"))
        {
            currentColor = "Pink";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Violet"))
        {
            currentColor = "Violet";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Yellow"))
        {
            currentColor = "Yellow";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Light Blue("))
        {
            currentColor = "Light Blue";
            colorTxt.text = currentColor;

        }
    }




}