using UnityEngine;
using UnityEngine.UI;



public class TwoMotorControl : MonoBehaviour
{
    //UI elements
    public Text rMotSpeed;
    public Text lMotSpeed;


    private float leftThrottleValue = 0f;
    private float rightThrottleValue = 0f;
    private Rigidbody tankRigidbody;

    public float rightMotorSpeed = 0f;
    public float leftMotorSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        tankRigidbody = GetComponent<Rigidbody>();
        lMotSpeed.text = "0";
        rMotSpeed.text = "0";
    }


    public void setMotorSpeeds(float right, float left)
    {
        rightMotorSpeed = right;
        leftMotorSpeed = left;





    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rMotSpeed.text = rightMotorSpeed.ToString();
        lMotSpeed.text = leftMotorSpeed.ToString();

        leftThrottleValue = leftMotorSpeed;
        rightThrottleValue = rightMotorSpeed;



        // Move the tank.
        Vector3 movement = transform.forward * ((leftThrottleValue + rightThrottleValue) / 2f) * Time.deltaTime;
        tankRigidbody.MovePosition(tankRigidbody.position + movement);

        // Turn the tank.
        float turn = (leftThrottleValue - rightThrottleValue) * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        tankRigidbody.MoveRotation(tankRigidbody.rotation * turnRotation);



    }
}