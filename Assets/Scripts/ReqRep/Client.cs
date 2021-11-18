using UnityEngine;
using System;
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
        GameObject motorInfoObj;
        TwoMotorControl motorController;


        //Access to the color script from this one
        public GameObject colorInfoObj;
        ColorDetection colorController;

        //Access to hub position/rotation script
        GameObject hubInfoObj;
        CalculatePosition hubController;

        //Access to the distance sensor
        public GameObject distanceInfo;
        DistanceSensor distanceController;

        //Access to the lightMatrix
        GameObject lightMatrixInfo;
        LightMatrix lightMatrixController;



        //Variables for RMotor
        float rMotorAmmount;
        float rMotorRotation;
        float rMotorDefault;
        float rMotorSpeed;
        string rMotorUnit;
        int rMotorSteering; // only for motor pairs
        string rMotorStall;
        string rMotorStop;


        //Variables for LMotor
        float lMotorAmmount;
        float lMotorRotation;
        float lMotorDefault;
        float lMotorSpeed;
        string lMotorUnit;
        int lMotorSteering; //only for motor pairs
        string lMotorStall;
        string lMotorStop;




        private Listener _listener;


        private void Start()
        {

            //GameObject spikePrime = GameObject.Find("SpikePrime");
            GameObject spikePrime = GameObject.FindWithTag("SpikePrime"); // changed this line to findWithTag because it finds the ACTIVE game object with this tag.
            _listener = new Listener(host, port, handleMessage);
            EventManager.Instance.onSendRequest.AddListener(OnClientRequest);

            motorInfoObj = spikePrime;
            motorController = motorInfoObj.GetComponent<TwoMotorControl>();


            colorController = colorInfoObj.GetComponent<ColorDetection>();

            hubInfoObj = spikePrime;
            hubController = hubInfoObj.GetComponent<CalculatePosition>();

            
            distanceController = distanceInfo.GetComponent<DistanceSensor>();

            lightMatrixInfo = GameObject.Find("LightMatrixImage");
            lightMatrixController = lightMatrixInfo.GetComponent<LightMatrix>();




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

        private void handleMessage(string newMessage)
        {


            Message message = Message.CreateFromJSON(newMessage);

            if (message != null)
            {

                if (message.lightMatrixMessage.isNotNull())
                {
                    handleLightMatrixMessage(message.lightMatrixMessage);
                }

                if (message.motorMessage.isNotNull())
                {
                    handleMotorMessage(message.motorMessage);
                }
            }

        }

        private void handleMotorMessage(MotorMessage motorMessage)
        {

            try
            {
                string motorId = motorMessage.getId();
                if (motorId.Equals("B"))
                {
                    this.rMotorAmmount = motorMessage.getAmount();
                    this.rMotorRotation = motorMessage.getRotation();
                    this.rMotorSpeed = motorMessage.getSpeed();
                    this.rMotorUnit = motorMessage.getUnit();
                    this.rMotorStop = motorMessage.getStopped();
                    this.rMotorDefault = motorMessage.getDefaultSpeed();

                }
                else if (motorId.Equals("A"))
                {
                    this.lMotorAmmount = motorMessage.getAmount();
                    this.lMotorRotation = motorMessage.getRotation();
                    this.lMotorSpeed = motorMessage.getSpeed();
                    this.lMotorUnit = motorMessage.getUnit();
                    this.lMotorStop = motorMessage.getStopped();
                    this.lMotorDefault = motorMessage.getDefaultSpeed();

                }

                motorController.setDefaultSpeeds(rMotorDefault, lMotorDefault);

                motorController.setMotorStop(rMotorStop, lMotorStop);

                motorController.setMotorSpeeds(rMotorSpeed, lMotorSpeed);
            }

            catch (Exception exception)
            {
                Debug.Log("Error incountered");
            }

        }

        private void handleLightMatrixMessage(LightMatrixMessage lightMatrixMessage)
        {

            lightMatrixController.setImage(lightMatrixMessage.image);

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

            info = (

                "{" + "\"color\" : {\"currentColor\" : \"" + colorController.currentColor + "\"" + "}}," +
                "{" + "\"motor\" : {\"stall\" : \"" + motorController.getIsStalled() + "\",\"currentPosition\" : \"" + hubController.getCurrentPosition() + "\"" + "}}," + //{"motor" : {"stall" : "false", "currentPosition : "123123"}},
                "{" + "\"hub\" : {\"rotation\" : \"" + hubController.getCurrentRotation().ToString("F0") + "\"" + "}}," +
                "{" + "\"distance\" : {\"value\" : \"" + distanceController.getCurrentDistance() + "\"" + "}}"


                     );

            return info;
        }

        [System.Serializable]
        public class Message
        {

            public LightMatrixMessage lightMatrixMessage = null;
            public MotorMessage motorMessage = null;

            public static Message CreateFromJSON(string jsonString)
            {
                try
                {
                    return JsonUtility.FromJson<Message>(jsonString);
                }
                catch (Exception exception)
                {
                    return null;
                }

            }
        }

        [System.Serializable]
        public class MotorMessage
        {

            public string id;
            public float amount;
            public float rotation;
            public float speed;
            public float defaultSpeed;
            public string unit;
            public string steering;
            public string stall;
            public string isStop;


            public string getId()
            {
                return this.id;
            }

            public float getAmount()
            {
                return this.amount;
            }

            public float getRotation()
            {
                return this.rotation;
            }

            public float getSpeed()
            {
                return this.speed;
            }

            public float getDefaultSpeed()
            {
                return this.defaultSpeed;
            }

            public string getUnit()
            {
                return this.unit;
            }

            public string getSteering()
            {

                return this.steering;
            }

            public string getStall()
            {

                return this.stall;
            }

            public string getStopped()
            {
                return this.isStop;
            }

            public bool isNotNull()
            {
                return this.id != null;
            }

        }

        [System.Serializable]
        public class LightMatrixMessage
        {
            public string image;
            public string text;

            public string getImage()
            {
                return this.image;
            }

            public string getText()
            {
                return this.text;
            }

            public bool isNotNull()
            {
                return this.image != null | this.text != null;
            }
        }

    }
}