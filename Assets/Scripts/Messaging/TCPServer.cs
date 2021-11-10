using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
public class TCPServer : MonoBehaviour
{

    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;


    //Access to the motor script from this one
    GameObject motorInfoObj;
    TwoMotorControl motorController;


    //Access to the color script from this one
    GameObject colorInfoObj;
    ColorDetection colorController;

    //Access to hub position/rotation script
    GameObject hubInfoObj;
    CalculatePosition hubController;

    //Access to the distance sensor
    GameObject distanceInfo;
    DistanceSensor distanceController;




    //Variables for RMotor
    float rMotorAmmount;
    float rMotorRotation;
    float rMotorDefault;
    float rMotorSpeed;
    string rMotorUnit;
    string rMotorStall;
    string rMotorStop;


    //Variables for LMotor
    float lMotorAmmount;
    float lMotorRotation;
    float lMotorDefault;
    float lMotorSpeed;
    string lMotorUnit;
    string lMotorStall;
    string lMotorStop;


    // Start is called before the first frame update
    void Start()
    {

        GameObject spikePrime = GameObject.Find("SpikePrime");

        motorInfoObj = spikePrime;
        motorController = motorInfoObj.GetComponent<TwoMotorControl>();

        colorInfoObj = GameObject.Find("ColorSensor");
        colorController = colorInfoObj.GetComponent<ColorDetection>();

        hubInfoObj = spikePrime;
        hubController = hubInfoObj.GetComponent<CalculatePosition>();

        distanceInfo = GameObject.Find("DistanceSensor");
        distanceController = distanceInfo.GetComponent<DistanceSensor>();


        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();

    }


    private void ListenForIncomingRequests()
    {

        try
        {

            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
            tcpListener.Start();
            Debug.Log("Original Server is listening");
            Byte[] bytes = new Byte[1024];


            while (true)
            {

                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {


                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {

                        int length = 0;

                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {

                            var incomingData = new byte[length];

                            Array.Copy(bytes, 0, incomingData, 0, length);

                            string clientMessage = Encoding.ASCII.GetString(incomingData);

                            Debug.Log("Client message recieved as: " + clientMessage);

                            //Message to json format
                            handleMessage(clientMessage);


                        }


                    }
                }

            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }

    }

    private void SendMessageToPython(string messageHeader)
    {

        if (connectedTcpClient != null)
        {
            try
            {
                // Get a stream object for writing.             
                NetworkStream stream = connectedTcpClient.GetStream();
                if (stream.CanWrite)
                {
                    //string serverMessage = "This is a message from your orginal server.";
                    // Convert string message to byte array.                 
                    byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(messageHeader);
                    // Write byte array to socketConnection stream.               
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                    //Debug.Log("Server sent his message - should be received by client");
                }

                
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
            }
        }

    }




    private void handleMessage(string newMessage)
    {
        //Debug.Log(newMessage);
        Message message = Message.CreateFromJSON(newMessage);

        if (message != null)
        {
           
            if (message.requestMessage != null) { 
                
                if (message.requestMessage.type == "color")
                {
                    SendMessageToPython("Recieved Color Message");
                }
                else
                {
                    SendMessageToPython("Type Test");

                }
            }
            else
            {
                SendMessageToPython("Test");
                
            }


            //if(message.MessageType != null)
            //{
            //    SendMessage("Yo");
            //}

            //if (message.colorMessage != null)
            //{
            //    Debug.Log("Color Request");
            //    //SendMessage("{" + "\"color\" : {\"currentColor\" : \"" + colorController.currentColor + "\"" + "}}");
            //    SendMessage("Test");
                
            //}

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

    [System.Serializable]
    public class Message
    {

        public RequestMessage requestMessage = null;
        public SendMessage sendMessage = null;

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
    public class RequestMessage
    {
        public string type = null;

        public static RequestMessage CreateFromJSON(string jsonString)
        {
            try
            {
                return JsonUtility.FromJson<RequestMessage>(jsonString);
            }
            catch (Exception exception)
            {
                return null;
            }

        }
    }


    [System.Serializable]
    public class SendMessage
    {
        public MotorMessage motorMessage = null;
        public ColorMessage colorMessage = null;

        public static SendMessage CreateFromJSON(string jsonString)
        {
            try
            {
                return JsonUtility.FromJson<SendMessage>(jsonString);
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
        public string messageType;

        public static MotorMessage createFromJSON(string jsonString)
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
    }

    [System.Serializable]
    public class ColorMessage
    {

        public string type;

        public static ColorMessage createFromJSON(string jsonString)
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