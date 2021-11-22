using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateCurrentRobotID : MonoBehaviour
{
    //Accessing the info for what the user is selecting
    public GameObject characters;
    public CharacterSelection characterController;


    //Accessing the global script that holds the current robot
    public GameObject characterHolder;
    public CurrentRobotTracker currentRobotController;

    private int currentRobotID;

    public Button myButton;




   void Start()
    {
        characters = GameObject.Find("Characters");
        characterController = characters.GetComponent<CharacterSelection>();
        currentRobotID = characterController.getSelectedCharacter();

        characterHolder = GameObject.Find("CurrentRobotHolder");
        currentRobotController = characterHolder.GetComponent<CurrentRobotTracker>();

        myButton.onClick.AddListener(SelectNewRobot);

    }
    

    void SelectNewRobot()
    {
        currentRobotID = characterController.getSelectedCharacter();
        currentRobotController.setCurrentRobotID(currentRobotID);
    }


}
