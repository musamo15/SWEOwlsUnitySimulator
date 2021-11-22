using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRobotTracker : MonoBehaviour
{


    public static CurrentRobotTracker currentRobot;


    public int currentRobotID = 0;

    void Awake()
    {
        if (currentRobot == null)
        {
            currentRobot = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyObject(gameObject);
        }

    }



    public void setCurrentRobotID(int i)
    {
        this.currentRobotID = i;
    }

    public int getCurrentRobotID()
    {
        return this.currentRobotID;
    }

}
