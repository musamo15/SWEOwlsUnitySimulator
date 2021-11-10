using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorDetection : MonoBehaviour
{

    bool colorFound;
    string currentColor = "None";
    public Text colorTxt;

    void Start()
    {
        colorTxt.text = currentColor;
    }




    void OnTriggerEnter(Collider other)
    {

        colorFound = true;

        if (other.name == "Red")
        {
            currentColor = "Red";
            colorTxt.text = currentColor;

        }

        if (other.name == "Black")
        {
            currentColor = "Black";
            colorTxt.text = currentColor;

        }

        if ((other.name.ToString()).Contains("White"))
        {
            currentColor = "White";
            colorTxt.text = currentColor;

        }
              if ((other.name.ToString()).Contains("Blue"))
                {
                    currentColor = "Blue";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Green"))
                {
                    currentColor = "Green";
                    colorTxt.text = currentColor;

                }

                if (other.name.ToString().Contains("Light Green"))
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

                if (other.name.ToString().Contains("Light Blue"))
                {
                    currentColor = "Light Blue";
                    colorTxt.text = currentColor;

                }
            }
   
    void OnTriggerExit(Collider other)
    {

        colorFound = false;
        currentColor = "None";
        colorTxt.text = currentColor;
    }

    void OnTriggerStay(Collider other)
    {

        colorFound = true;



        if (other.name == "Black")
        {
            currentColor = "Black";
            colorTxt.text = currentColor;

        }

        if (other.name == "Red")
        {
            currentColor = "Red";
            colorTxt.text = currentColor;

        }

        if ((other.name.ToString()).Contains("White"))
        {
            currentColor = "White";
            
            colorTxt.text = currentColor;

        }
        if ((other.name.ToString()).Contains("Blue"))
        {
            currentColor = "Blue";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Green"))
        {
            currentColor = "Green";
            colorTxt.text = currentColor;

        }

        if (other.name.ToString().Contains("Light Green"))
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

        if (other.name.ToString().Contains("Light Blue"))
        {
            currentColor = "Light Blue";
            colorTxt.text = currentColor;

        }
    }


}



