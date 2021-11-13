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
    public GameObject colorInfoObj;
    ColorDetection colorController;

    //Access to hub position/rotation script
    GameObject hubInfoObj;
    CalculatePosition hubController;

    //Access to the distance sensor
    public GameObject distanceInfo;
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

        colorController = colorInfoObj.GetComponent<ColorDetection>();

        hubInfoObj = spikePrime;
        hubController = hubInfoObj.GetComponent<CalculatePosition>();

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

                            //Debug.Log("Client message recieved as: " + clientMessage);

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
                    // Convert string message to byte array.                 
                    byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(messageHeader);
                    // Write byte array to socketConnection stream.               
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
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

        Debug.Log(newMessage);

        if (message != null)
        {
            string messageType = message.messageType;
            string messageBody = message.messageBody;

            if (messageType != null)
            {

                if (messageType.Equals("lightMatrix"))
                {
                    LightMatrixMessage lightMatrixMessage = LightMatrixMessage.createFromJSON(messageBody);
                    handleLightMatrixMessage(lightMatrixMessage);
                }

                else if (messageType.Equals("color"))
                {
                    ColorMessage colorMessage = ColorMessage.createFromJSON(messageBody);
                    handleColorMessage(colorMessage);
                }

                else if (messageType.Equals("motor"))
                {
                    MotorMessage motorMessage = MotorMessage.createFromJSON(messageBody);
                    handleMotorMessage(motorMessage);

                }

                else if (messageType.Equals("distance"))
                {
                    DistanceMessage distanceMessage = DistanceMessage.createFromJSON(messageBody);
                    handleDistanceMessage(distanceMessage);
                }
            }

        }

    }

    private void handleColorMessage(ColorMessage colorMessage)
    {
        //SendMessageToPython("Yo");
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

    private void handleLightMatrixMessage(LightMatrixMessage lightMatrixMessage)
    {

    }

    private void handleDistanceMessage(DistanceMessage distanceMessage)
    {

    }


    [System.Serializable]
    public class Message
    {
        public string messageType;
        public string messageBody;
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


    //[System.Serializable]
    //public class RequestMessage
    //{
    //    public string type = null;

    //    public static RequestMessage CreateFromJSON(string jsonString)
    //    {
    //        try
    //        {
    //            return JsonUtility.FromJson<RequestMessage>(jsonString);
    //        }
    //        catch (Exception exception)
    //        {
    //            return null;
    //        }

    //    }
    //}


    //[System.Serializable]
    //public class SendMessage
    //{

    //    public static SendMessage CreateFromJSON(string jsonString)
    //    {
    //        try
    //        {
    //            return JsonUtility.FromJson<SendMessage>(jsonString);
    //        }
    //        catch (Exception exception)
    //        {
    //            return null;
    //        }

    //    }
    //}




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

        private string colorID;

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


        public string getColorID()
        {
            return this.colorID;
        }



    }

    [System.Serializable]
    public class DistanceMessage
    {
        public static DistanceMessage createFromJSON(string jsonString)
        {
            try
            {
                return JsonUtility.FromJson<DistanceMessage>(jsonString);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }

    [System.Serializable]
    public class LightMatrixMessage
    {
        private string image;


        public static LightMatrixMessage createFromJSON(string jsonString)
        {
            try
            {
                return JsonUtility.FromJson<LightMatrixMessage>(jsonString);
            }
            catch (Exception exception)
            {
                return null;
            }

        }
    }
}