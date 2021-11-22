using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSensor : MonoBehaviour
{
    public float detectableDistance = 200; //200cm as default
    private float currentDistance = 200;
    private Ray collisionRay;

    public Text distanceText;
    public string id;




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
