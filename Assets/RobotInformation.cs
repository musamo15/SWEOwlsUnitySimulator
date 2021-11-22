using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotInformation : MonoBehaviour
{
    public Text robotName;
    public Text robotDistance;
    public Text robotColor;

    public int currentCharacter;

    public GameObject characters;

    CharacterSelection characterController;


    //0 is default
    //1 is distance sensors standing up
    //2 is motor front back and double color sensors
    //3 is 3 color sensors


    void Start()
    {
        characterController = characters.GetComponent<CharacterSelection>();

    }


    void FixedUpdate()
    {

        currentCharacter = characterController.getSelectedCharacter();
 
        if (currentCharacter == 0)
        {
            robotName.text = "Default";
            robotDistance.text = "C";
            robotColor.text = "D";
        }
        else if(currentCharacter == 1)
        {
            robotName.text = "Standing Distance";
            robotDistance.text = "D, F";
            robotColor.text = "C, E";
        }
        else if (currentCharacter == 2)
        {
            robotName.text = "Front/Back Distance";
            robotDistance.text = "C, F";
            robotColor.text = "E, D";
        }

        else if(currentCharacter == 3)
        {
            robotName.text = "Three Color";
            robotDistance.text = "None";
            robotColor.text = "C, D, E";
        }





    }







}
