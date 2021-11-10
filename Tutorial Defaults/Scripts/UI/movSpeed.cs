using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class movSpeed : MonoBehaviour
{

    public Text speedText;
    string currentSpeed;

    Vector3 PrevPos;
    Vector3 NewPos;
    Vector3 ObjVelocity;

    void Start()
    {
        PrevPos = transform.position;
        NewPos = transform.position;

    }

    void FixedUpdate()//24 fps 
    {
        NewPos = transform.position;  // each frame track the new position
        ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation
        currentSpeed = ObjVelocity.magnitude.ToString("F0");
        speedText.text = (ObjVelocity.magnitude.ToString("F0"));
        
    }

}
