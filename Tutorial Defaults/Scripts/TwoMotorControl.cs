using UnityEngine;
using UnityEngine.UI;



public class TwoMotorControl : MonoBehaviour
{
    //UI elements
    public Text rMotSpeed;
    public Text lMotSpeed;


    private Rigidbody robotRigidbody;


    //Motor information
    public float rightMotorSpeed = 0f;
    public float leftMotorSpeed = 0f;
    public bool isStalled;

    private float rightMotorRotateFor = 0f;
    private float leftMotorRotateFor = 0f;



    // Start is called before the first frame update
    void Start()
    {
        robotRigidbody = GetComponent<Rigidbody>();
        lMotSpeed.text = "0";
        rMotSpeed.text = "0";
        isStalled = false;
    }

    public void setMotorRotation(float right, float left)
    {
        rightMotorRotateFor = right;
        leftMotorRotateFor = left;
    }

    //Setters and getters
    public void setMotorSpeeds(float right, float left)
    {
            rightMotorSpeed = right;
            leftMotorSpeed = left;

    }

    public void setIsStalled(bool x)
    {
        isStalled = x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If stalled
        if(isStalled)
        {
            setMotorSpeeds(0, 0);
        }




        //Updating UI
        rMotSpeed.text = rightMotorSpeed.ToString();
        lMotSpeed.text = leftMotorSpeed.ToString();



        // Move the robot.
        Vector3 movement = transform.forward * ((leftMotorSpeed + rightMotorSpeed) / 2f) * Time.deltaTime;
        robotRigidbody.MovePosition(robotRigidbody.position + movement);

        // Turn the robot.
        float turn = (leftMotorSpeed - rightMotorSpeed) * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        robotRigidbody.MoveRotation(robotRigidbody.rotation * turnRotation);



    }
}