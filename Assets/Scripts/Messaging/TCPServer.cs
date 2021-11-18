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

    //Access to the lightMatrix
    GameObject lightMatrixInfo;
    LightMatrix lightMatrixController;




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


    void OnApplicationQuit()
    {
        tcpListener.Stop();
        tcpListenerThread.Abort();
    }


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


        lightMatrixInfo = GameObject.Find("LightMatrixImage");
        lightMatrixController = lightMatrixInfo.GetComponent<LightMatrix>();


        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();

    }


    private void ListenForIncomingRequests()
    {

        try
        {

            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 3000);
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
        
        Message message = Message.CreateFromJSON(newMessage);
        

        if (message != null)
        {
            string messageType = message.messageType;
            string messageRequestType = message.messageRequestType;

            if (messageType != null)
            {
                if(messageType.Equals("shutdown"))
                {
                    Application.Quit();
                }

                else if (messageType.Equals("hub"))
                {
                    HubMessage hubMessage = HubMessage.createFromJSON(newMessage);
                    handleHubMessage(hubMessage);
                }


                else if (messageType.Equals("lightMatrix"))
                {
                    LightMatrixMessage lightMatrixMessage = LightMatrixMessage.createFromJSON(newMessage);
                    handleLightMatrixMessage(lightMatrixMessage);
                }

                else if (messageType.Equals("color"))
                {
                    ColorMessage colorMessage = ColorMessage.createFromJSON(newMessage);
                    handleColorMessage(colorMessage);
                }

                else if (messageType.Equals("motor"))
                {
                    MotorMessage motorMessage = MotorMessage.createFromJSON(newMessage);
                    handleMotorMessage(motorMessage);

                }

                else if (messageType.Equals("distance"))
                {
                    DistanceMessage distanceMessage = DistanceMessage.createFromJSON(newMessage);
                    handleDistanceMessage(distanceMessage);
                }

                if(messageRequestType.Equals("Send"))
                {
                    SendMessageToPython("Messsage Received");
                }
            }

        }

    }

    private void handleColorMessage(ColorMessage colorMessage)
    {
        SendMessageToPython("{" + "\"color\" : {\"currentColor\" : \"" + colorController.currentColor + "\"" + "}}");
    }

    private void handleMotorMessage(MotorMessage motorMessage)
    {


        string motorId = motorMessage.getId();


        if (motorMessage.messageRequestType.Equals("Request") && motorMessage.component.Equals("stall"))
        {
            SendMessageToPython("{" + "\"motor\" : {\"stall\" : \"" + motorController.getIsStalled() + "\"}}");
        }

        if (motorMessage.messageRequestType.Equals("Request") && motorMessage.component.Equals("position"))
        {
            SendMessageToPython("{" + "\"motor\" : {\"currentPosition\" : \"" + hubController.getCurrentPosition() + "\"}}");
        }

        else
        {


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

    }

    private void handleLightMatrixMessage(LightMatrixMessage lightMatrixMessage)
    {
        lightMatrixController.setImage(lightMatrixMessage.getImage());
    }

    private void handleDistanceMessage(DistanceMessage distanceMessage)
    {

    }

    private void handleHubMessage(HubMessage hubMessage)
    {

            SendMessageToPython("{" + "\"hub\" : {\"rotation\" : \"" + hubController.getCurrentRotation().ToString("F0") + "\"" + "}}");

    }


    [System.Serializable]
    public class Message
    {
        public string messageType;
        public string messageRequestType;
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
        public string messageRequestType;
        public string component;

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
        private string text;

        public string getImage()
        {
            return this.image;
        }

        public string getText()
        {
            return this.text;
        }


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

    [System.Serializable]
    public class HubMessage
    {
        public static HubMessage createFromJSON(string jsonString)
        {
            try
            {
                return JsonUtility.FromJson<HubMessage>(jsonString);
            }
            catch (Exception exception)
            {
                return null;
            }

        }
    }
}