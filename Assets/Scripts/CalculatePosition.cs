using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CalculatePosition : MonoBehaviour
{
    //Calculate rotation and position relative to the starting position 
    public Vector3 startingPosition; //changed this from private to public to use with levelEditorManager
    private Vector3 currentPosition;

    private float startingRotation;
    private float currentRotation;

    public Text currentPositionText;
    public Text currentYawText;

    public GameObject SpikePrime; //changed this to public

    public void Start()
    {
        SpikePrime = GameObject.FindWithTag("SpikePrime");
        //SpikePrime = GameObject.FindGameObjectWithTag("SpikePrime");
        startingPosition = SpikePrime.transform.position;
        startingRotation = SpikePrime.transform.eulerAngles.y;
    }

    public float getCurrentRotation()
    {
        return this.currentRotation;
    }

    public Vector3 getCurrentPosition()
    {
        return this.currentPosition;
    }
    //Getting current position RELATIVE TO STARTING POSITION
    public Vector3 calculateCurrentPosition()
    {
        Vector3 currentPos = SpikePrime.transform.position;

        currentPositionText.text = (currentPos-startingPosition).ToString();
        
        return currentPos-startingPosition;
    }


    public float calculateCurrentRotation()
    {

        currentRotation = SpikePrime.transform.eulerAngles.y;




        //Euler agnles go from -90 to + 270??????????????????????????????????????????????????? WHY IS THIS A THING??????????????????????

        float curCacled = currentRotation - startingRotation;

        if (curCacled < 0)
        {
            currentRotation = (curCacled) + 360;
        }
        else
        {
            currentRotation = curCacled;
        }

        currentYawText.text = (currentRotation).ToString("F0");
        return currentRotation;
    }

    public void Update()
    {
        currentPosition = calculateCurrentPosition();
        currentRotation = calculateCurrentRotation();
        
    }
    


}
