using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSensor : MonoBehaviour
{
    //Setting the robot speeds to 10 and 1 for some reason break this i have no idea why?
    // it works in every other case. possibly something wrong with the updating? When it turns it goes down to 0 back up to 4 and then back down again 
    public float detectableDistance = 200; //200cm as default
    private float currentDistance = 99999999;
    private Ray collisionRay;

    private GameObject distanceSensor;

    public Text distanceText;

    public void Start()
    {
        distanceSensor = GameObject.Find("DistanceSensor");
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        collisionRay = new Ray(transform.position, -transform.right);
        Debug.DrawRay(transform.position, -transform.right * detectableDistance);

        if (Physics.Raycast(collisionRay, out hit, detectableDistance))
        {

            currentDistance = hit.distance;
            distanceText.text = currentDistance.ToString();
        }

        else
        {
            distanceText.text = "200";
        }

    }


    public string getCurrentDistance()
    {
        if (this.currentDistance != null)
        {
            return this.currentDistance.ToString("");
        }
        else
        {
            return "200";
        }
        
    }

}
