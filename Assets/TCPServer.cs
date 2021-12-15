using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TCPServer : MonoBehaviour
{

    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;

    public bool goToMain = false;


    //Access to the script that contains the current robot
    public GameObject characterHolder;
    public CurrentRobotTracker currentRobotController;

    private int currentRobotID;

    GameObject spikePrime;


    //Access to the motor script from this one
    GameObject motorInfoObj;
    TwoMotorControl motorController;



    //Access to hub position/rotation script
    GameObject hubInfoObj;
    CalculatePosition hubController;


    //Access to the lightMatrix
    GameObject lightMatrixInfo;
    LightMatrix lightMatrixController;


    //*****Default Robot Access*****

        //Access to the distance sensor
        public GameObject defaultDistanceInfo;
        DistanceSensor defaultDistanceController;

    
        //Access to the color script from this one
        public GameObject defaultColorInfoObj;
        ColorDetection defaultColorController;

    //*****Front Back Robot Access*****

        //Access to the distance sensor
        public GameObject frontBackDistanceInfoC;
        DistanceSensor frontBackDistanceControllerC;

        public GameObject frontBackDistanceInfoF;
        DistanceSensor frontBackDistanceControllerF;
    

    //Access to the color script from this one
        public GameObject frontBackColorInfoObjE;
        ColorDetection frontBackColorControllerE;

        public GameObject frontBackColorInfoObjD;
        ColorDetection frontBackColorControllerD;

    //*****StandingDistance Robot Access*****

        //Access to the distance sensor
        public GameObject standingDistanceInfoD;
        DistanceSensor standingDistanceControllerD;

        public GameObject standingDistanceInfoF;
        DistanceSensor standingDistanceControllerF;


        //Access to the color script from this one
        public GameObject standingColorInfoObjE;
        ColorDetection standingColorControllerE;

        public GameObject standingColorInfoObjC;
        ColorDetection standingColorControllerC;


    //*****TrippleColorRobot Access*****
        public GameObject trippleColorInfoObjC;
        ColorDetection trippleColorControllerC;

        public GameObject trippleColorInfoObjD;
        ColorDetection trippleColorControllerD;

        public GameObject trippleColorInfoObjE;
        ColorDetection trippleColorControllerE;

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
        closeTCPSocketConnection();
        tcpListenerThread.Abort();
    }

    public void closeTCPSocketConnection()
    {
        if (tcpListener != null)
        {

            tcpListener.Stop();
            tcpListener.Server.Close();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Color Sensors
        defaultColorController = defaultColorInfoObj.GetComponent<ColorDetection>();

        //Standings
        standingColorControllerC = standingColorInfoObjC.GetComponent<ColorDetection>();
        standingColorControllerE = standingColorInfoObjE.GetComponent<ColorDetection>();

        //FrontBacks
        frontBackColorControllerE = frontBackColorInfoObjE.GetComponent<ColorDetection>();
        frontBackColorControllerD = frontBackColorInfoObjD.GetComponent<ColorDetection>();

        //Distance sensors
        //Default
        defaultDistanceController = defaultDistanceInfo.GetComponent<DistanceSensor>();

        //Standing
        standingDistanceControllerD = standingDistanceInfoD.GetComponent<DistanceSensor>();
        standingDistanceControllerF = standingDistanceInfoF.GetComponent<DistanceSensor>();

        //Front Back
        frontBackDistanceControllerC = frontBackDistanceInfoC.GetComponent<DistanceSensor>();
        frontBackDistanceControllerF = frontBackDistanceInfoF.GetComponent<DistanceSensor>();

        //TripleColor
        trippleColorControllerC = trippleColorInfoObjC.GetComponent<ColorDetection>();
        trippleColorControllerD = trippleColorInfoObjD.GetComponent<ColorDetection>();
        trippleColorControllerE = trippleColorInfoObjE.GetComponent<ColorDetection>();



        characterHolder = GameObject.Find("CurrentRobotHolder");
        currentRobotController = characterHolder.GetComponent<CurrentRobotTracker>();



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
                        try
                        {


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
                        catch (Exception exception)
                        {
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

                    //Debug.Log("Message Sent");

                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                    //Debug.Log(messageHeader);
                }

                
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception" + socketException);
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
            currentRobotID = currentRobotController.getCurrentRobotID();

           

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

                else if (messageType.Equals("distanceSensor"))
                {
                    DistanceMessage distanceMessage = DistanceMessage.createFromJSON(newMessage);
                    handleDistanceMessage(distanceMessage);
                }

                if(messageRequestType.Equals("Send"))
                {
                    SendMessageToPython("{\"message\" : \"Message received\"}");
                }
            }

        }

    }

    private void handleColorMessage(ColorMessage colorMessage)
    {
        
        if(currentRobotID == 0)
        {
            SendColorMessage(defaultColorController.currentColor);
        }

        else if (currentRobotID == 1)
        {
            //STanding
            if (colorMessage.getColorID() == "C")
            {
                SendColorMessage(standingColorControllerC.currentColor);
            }


            else if(colorMessage.getColorID() == "E")
            {
                SendColorMessage(standingColorControllerE.currentColor);
            }

            else
            {
                SendColorPortErrorMessage("C, F", colorMessage.id);
            }

        }
        //Front Back
        else if (currentRobotID == 2)
        {
            if (colorMessage.getColorID().Equals("E"))
            {
                SendColorMessage(frontBackColorControllerE.currentColor);
            }


            else if (colorMessage.getColorID().Equals("D"))
            {
                SendColorMessage(frontBackColorControllerD.currentColor);
            }

            else
            {
                SendColorPortErrorMessage("E, D", colorMessage.id);
            }
        }

        //Triple color
        else if(currentRobotID == 3)
        {
            if(colorMessage.getColorID().Equals("C"))
            {
                //Debug.Log("????");
                //Debug.Log(trippleColorControllerC.colorTxt.text);
                SendColorMessage(trippleColorControllerC.currentColor);
                
            }
            else if (colorMessage.getColorID().Equals("D"))
            {
                SendColorMessage(trippleColorControllerD.currentColor);
            }
            else if (colorMessage.getColorID().Equals("E"))
            {
                SendColorMessage(trippleColorControllerE.currentColor);
            }
            else
            {
                SendColorPortErrorMessage("C, D, E", colorMessage.id);
            }
        }
    }

    public void SendColorMessage(string currentColor)
    {
        SendMessageToPython("{" + "\"color\" : {\"currentColor\" : \"" + currentColor + "\"" + "}}");
    }

    public void SendColorPortErrorMessage(string ports, string id)
    {
        SendMessageToPython("{" + "\"error\" : {\"message\" : \"" + "Tried to access unavailable port " + id + " with a Color Sensor. Currently available ports are: " + ports  + "\"" + "}}");
        goToMain = true;

    }

    public void SendDistancePortErrorMessage(string ports, string id)
    {
        SendMessageToPython("{" + "\"error\" : {\"message\" : \"" + "Tried to access unavailable port " + id +" with a Distance Sensor. Currently available ports are: " + ports + "\"" + "}}");
        goToMain = true;
    }

    private void handleMotorMessage(MotorMessage motorMessage)
    {


        string motorId = motorMessage.getId();


        if (motorMessage.messageRequestType.Equals("Request"))
        {

            if (motorMessage.component.Equals("stall"))
            {
                SendMessageToPython("{" + "\"motor\" : {\"stall\" : \"" + motorController.getIsStalled() + "\"}}");
            }

            else if (motorMessage.component.Equals("position"))
            {
                SendMessageToPython("{" + "\"motor\" : {\"currentPosition\" : \"" + hubController.getCurrentPosition() + "\"}}");
            }
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
        lightMatrixInfo = GameObject.Find("LightMatrixImage");
        lightMatrixController = lightMatrixInfo.GetComponent<LightMatrix>();
        lightMatrixController.setImage(lightMatrixMessage.getImage());
    }

    private void handleDistanceMessage(DistanceMessage distanceMessage)
    {

        if (currentRobotID == 0)
        {
            SendMessageToPython("{" + "\"distanceSensor\" : {\"value\" : \"" + defaultDistanceController.getCurrentDistance() + "\"" + "}}");
        }

        else if (currentRobotID == 1)
        {
            if (distanceMessage.getDistanceID().Equals("D"))
            {
                SendMessageToPython("{" + "\"distanceSensor\" : {\"value\" : \"" + standingDistanceControllerD.getCurrentDistance() + "\"" + "}}");
            }
            else if (distanceMessage.getDistanceID().Equals("F"))
            {
                SendMessageToPython("{" + "\"distanceSensor\" : {\"value\" : \"" + standingDistanceControllerF.getCurrentDistance() + "\"" + "}}");
            }
            else
            {
                SendDistancePortErrorMessage("D, F", distanceMessage.id);
            }
            
        }

        else if(currentRobotID == 2)
        {
            if (distanceMessage.getDistanceID().Equals("C"))
            {
                SendMessageToPython("{" + "\"distanceSensor\" : {\"value\" : \"" + frontBackDistanceControllerC.getCurrentDistance() + "\"" + "}}");
            }
            else if (distanceMessage.getDistanceID().Equals("F"))
            {
                SendMessageToPython("{" + "\"distanceSensor\" : {\"value\" : \"" + frontBackDistanceControllerF.getCurrentDistance() + "\"" + "}}");
            }
            else
            {
                SendDistancePortErrorMessage("C, F", distanceMessage.id);
            }
        }

        else if (currentRobotID == 3)
        {
            SendDistancePortErrorMessage("None", distanceMessage.id);
        }
    }

    private void handleHubMessage(HubMessage hubMessage)
    {

            SendMessageToPython("{" + "\"hub\" : {\"rotation\" : \"" + hubController.getCurrentRotation().ToString("F0") + "\"" + "}}");

    }


    void Update()
    {
        if(spikePrime == null)
        {
            spikePrime = GameObject.FindGameObjectWithTag("SpikePrime");
            motorInfoObj = spikePrime;

            try
            {
                motorController = motorInfoObj.GetComponent<TwoMotorControl>();
            }
            catch(Exception e)
            {

            }
            
        }

        if(goToMain)
        {
            SceneManager.LoadScene("MainMenu");
            goToMain = false;
        }

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

        public string id;

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
            return id.ToUpper();
        }



    }

    [System.Serializable]
    public class DistanceMessage
    {

        public string id;
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

        public string getDistanceID()
        {
            return id.ToUpper();
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