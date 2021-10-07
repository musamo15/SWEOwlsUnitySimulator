using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using UnityEngine.UI;



namespace ReqRep
{


    public class Client : MonoBehaviour
    {
        [SerializeField] private string host;
        [SerializeField] private string port;


        //Access to the motor script from this one
        GameObject motorInfObj;
        TwoMotorControl controller;



        //UI elements containing object information
        public Text currentColor;
        public Text currentSpeed;


        //Strings passed to listener
        string curSpeed = "";
        string curColor = "";


        //Variables for RMotor
        float rMotorAmmount;
        float rMotorRotation;
        public float rMotorSpeed;
        string rMotorUnit;
        int rMotorSteering;
        bool rMotorStall;

        //Variables for LMotor
        float lMotorAmmount;
        float lMotorRotation;
        public float lMotorSpeed;
        string lMotorUnit;
        int lMotorSteering;
        bool lMotorStall;


        private Listener _listener;


        private void Start()
        {
            _listener = new Listener(host, port, HandleMessage);
            EventManager.Instance.onSendRequest.AddListener(OnClientRequest);

            motorInfObj = GameObject.Find("SpikePrime");
            controller = motorInfObj.GetComponent<TwoMotorControl>();

            //Making a new thread because running request loop on the same thread causes massive preformance issues
            Thread requestThread = new Thread(SendRequest);
            requestThread.Name = "requestThread";
            requestThread.Start();
            requestThread.IsBackground = true;
        }

        private void OnClientRequest()
        {
            EventManager.Instance.onClientBusy.Invoke();
            _listener.RequestMessage();
            EventManager.Instance.onClientFree.Invoke();

        }



        private void HandleMessage(string message)
        {
            Debug.Log(message);
            string[] words = message.Split(' ');
            if (words[1].Contains("motor"))
            {



                if (words[3].Contains("A")) //LeftMotor
                {
                    
                    rMotorAmmount = float.Parse(words[5]);
                    rMotorRotation = float.Parse(words[7]);
                    rMotorSpeed = float.Parse(words[9]);
                    rMotorUnit = words[11];
                    //rMotorStall = bool.Parse(words[15]);
                }

                if (words[3].Contains("B")) //LeftMotor
                {
                    lMotorAmmount = float.Parse(words[5]);
                    lMotorRotation = float.Parse(words[7]);
                    lMotorSpeed = float.Parse(words[9]);
                    lMotorUnit = words[11];
                    //lMotorStall = bool.Parse(words[15]);
                }
                
                
                
                Debug.Log("Message Processed");
                controller.setMotorSpeeds(rMotorSpeed, lMotorSpeed);
                
            }

        }







        void SendRequest()
        {

            while (true)
            {
                _listener.information = getInfo();
                EventManager.Instance.onSendRequest.Invoke();
            }

        }

        string getInfo()
        {
            string info;

            curColor = currentColor.text;
            curSpeed = currentSpeed.text;

            info = ("{{'type' : 'color', 'color' : '" + curColor + "'}}");//color, rm info, lm info, hub info.

            return info;
        }


    }

    
}