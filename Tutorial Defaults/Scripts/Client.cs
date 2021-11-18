using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using UnityEngine.UI;
using Newtonsoft.Json;


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



        private void HandleMessage(string newMessage)
        {
            Debug.Log(newMessage);
            Message message = Message.CreateFromJSON(newMessage);

            if (message != null)
            {
                if (message.motorMessage != null)
                {
                    handleMotorMessage(message.motorMessage);
                }
            }

        }

        private void handleMotorMessage(MotorMessage motorMessage)
        {
            string motorId = motorMessage.getId();
            if (motorId.Equals("B"))
            {
                this.rMotorAmmount = motorMessage.getAmount();
                this.rMotorRotation = motorMessage.getRotation();
                this.rMotorSpeed = motorMessage.getSpeed();
                this.rMotorUnit = motorMessage.getUnit();
                Debug.Log("Made it here");
            }
            else if (motorId.Equals("A"))
            {
                this.lMotorAmmount = motorMessage.getAmount();
                this.lMotorRotation = motorMessage.getRotation();
                this.lMotorSpeed = motorMessage.getSpeed();
                this.lMotorUnit = motorMessage.getUnit();

            }

            controller.setMotorSpeeds(rMotorSpeed, lMotorSpeed);

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

            info = (
                "{" + "\"color\" : {\"currentColor\" : \"" + curColor + "\"" + "}}" //{"color" :

                     /*
                         "{ \"motor\" : {\"ammount\" : " + 0 + ","+ //replace 0 with varialbes
                         "\"rotat"


                         +"}" */
                     ); //color, rm info, lm info, hub info.

            return info;
        }


    }

    [System.Serializable]
    public class Message
    {

        public MotorMessage motorMessage = null;
        public ColorMessage colorMessage = null;

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
        public string unit;
        public string steering;
        public string stall;

        public static MotorMessage CreateFromJSON(string jsonString)
        {
            try
            {
                return JsonUtility.FromJson<MotorMessage>(jsonString);
            }
            catch (Exception exception)
            {
                return null;
            }

        }

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
    }

    [System.Serializable]
    public class ColorMessage
    {

        public string id;

        public static ColorMessage CreateFromJSON(string jsonString)
        {
            try
            {
                return JsonUtility.FromJson<ColorMessage>(jsonString);
            }
            catch (Exception exception)
            {
                return null;
            }

        }
    }
}