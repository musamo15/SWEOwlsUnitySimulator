using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisions : MonoBehaviour
{
    GameObject motorInfo;
    TwoMotorControl controller;

    void OnTriggerEnter(Collider other)
    {
        motorInfo = GameObject.FindWithTag("SpikePrime");
        controller = motorInfo.GetComponent<TwoMotorControl>();
        controller.setIsStalled(true);

    }


}
