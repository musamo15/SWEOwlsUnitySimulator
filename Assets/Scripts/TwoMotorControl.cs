using UnityEngine;
using UnityEngine.UI;



public class TwoMotorControl : MonoBehaviour
{
    //UI elements
    public Text rMotorSpeed;
    public Text lMotorSpeed;


    private Rigidbody robotRigidBody;


    //Motor information
    public float rightMotorSpeed = 0f;
    public float leftMotorSpeed = 0f;

    private float rightDefaultSpeed = 0f;
    private float leftDefaultSpeed = 0f;

    private bool lMotorStop;
    private bool rMotorStop;

    public bool isStalled;


    //"Noise" for randomness on motors
    private float rightMotorNoise;
    private float leftMotorNoise;



    // Start is called before the first frame update
    void Start()
    {
        robotRigidBody = GetComponent<Rigidbody>();
        lMotorSpeed.text = "0";
        rMotorSpeed.text = "0";

        isStalled = false;

        lMotorStop = false;
        rMotorStop = false;
        rightMotorNoise = Random.Range(-0.009f, 0.009f);
        leftMotorNoise = Random.Range(-0.009f, 0.009f);


    }


    //Setters and getters
    public void setMotorSpeeds(float right, float left)
    {
            rightMotorSpeed = right + (rightMotorNoise);
            leftMotorSpeed = left + (leftMotorNoise);

    }

    public void setDefaultSpeeds(float right, float left)
    {
        rightDefaultSpeed = right;
        leftDefaultSpeed = left;

    }

    public void setMotorStop(string right, string left)
    {
        if(left != null)
        {
         if(left.Contains("true"))
                {
                    lMotorStop = true;
                }
        }
       
        if(right != null)
        {

            if(right.Contains("true"))
            {
             rMotorStop = true;
            }
        }
        
    }

    public void setIsStalled(bool x)
    {
        isStalled = x;
    }

   public bool getIsStalled()
    {
        return isStalled;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //If stalled
        if(isStalled)
        {
            setMotorSpeeds(0 -rightMotorNoise, 0 - leftMotorNoise);
        }

        if(lMotorStop)
        {
            leftMotorSpeed = 0 - leftMotorNoise;
            lMotorStop = false;

        }

        if(rMotorStop)
        {
            rightMotorSpeed = 0 - rightMotorNoise;
            rMotorStop = false;
        }




        //Updating UI
        rMotorSpeed.text = rightMotorSpeed.ToString("F3");
        lMotorSpeed.text = leftMotorSpeed.ToString("F3");



        //Move the robot.
        Vector3 movement = -1 *transform.forward * ((leftMotorSpeed + rightMotorSpeed) / 2f) * Time.deltaTime;
        robotRigidBody.MovePosition(robotRigidBody.position + movement);

        // Turn the robot.
        float turn = -1 *(rightMotorSpeed - leftMotorSpeed ) * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        robotRigidBody.MoveRotation(robotRigidBody.rotation * turnRotation);



    }
}