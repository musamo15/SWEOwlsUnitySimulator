using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRobot : MonoBehaviour
{
    //Accessing the global script that holds the current robot
    public GameObject characterHolder;
    public CurrentRobotTracker currentRobotController;

    //Objects for each robot type UI and spike prime
    //Default:
    public GameObject defaultRobot;
    public GameObject defaultUI;


    //Front-Back Robot:
    public GameObject frontBackRobot;
    public GameObject frontBackUI;


    //Standing distance sensors
    public GameObject standingDistanceRobot;
    public GameObject standingDistanceUI;

    //TripleColor
    public GameObject trippleColorRobot;
    public GameObject trippleColorUI;




    private int currentRobotID;

    void Awake()
    {
        characterHolder = GameObject.Find("CurrentRobotHolder");
        currentRobotController = characterHolder.GetComponent<CurrentRobotTracker>();
        SetAllFalse();

    }


    void FixedUpdate()
    {
        
        currentRobotID = currentRobotController.getCurrentRobotID();

        //Default
        if (currentRobotID == 0 && !defaultRobot.activeSelf)
        {
            SetAllFalse();
            defaultRobot.SetActive(true);
            defaultUI.SetActive(true);
           
        }

         
        else if(currentRobotID == 1 && !standingDistanceRobot.activeSelf)
        {
            SetAllFalse();
            standingDistanceRobot.SetActive(true);
            standingDistanceUI.SetActive(true);
            
        }

        //FrontBack 
        else if (currentRobotID == 2 && !frontBackRobot.activeSelf)
        {
            SetAllFalse();
            frontBackRobot.SetActive(true);
            frontBackUI.SetActive(true);
        }

        else if(currentRobotID == 3 && !trippleColorRobot.activeSelf)
        {
            SetAllFalse();
            trippleColorRobot.SetActive(true);
            trippleColorUI.SetActive(true);
        }

    }

    public void SetAllFalse()
    {
        //Default
        defaultRobot.SetActive(false);
        defaultUI.SetActive(false);


        //Front-Back Robot:
        frontBackRobot.SetActive(false);
        frontBackUI.SetActive(false);

        //Standing distance
        standingDistanceRobot.SetActive(false);
        standingDistanceUI.SetActive(false);

        //Tripple color
        trippleColorRobot.SetActive(false);
        trippleColorUI.SetActive(false);
}



}
