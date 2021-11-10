using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisions : MonoBehaviour
{
    GameObject motorInfo;
    TwoMotorControl controller;

    public void Start()
    {


    motorInfo = GameObject.Find("SpikePrime");
    controller = motorInfo.GetComponent<TwoMotorControl>();

    }
   

    void OnTriggerEnter(Collider other)
    {
        controller.setIsStalled(true);

    }


}
