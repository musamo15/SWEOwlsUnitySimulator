using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TCPTestClient : MonoBehaviour
{
		
	public TcpClient socketConnection;
	public Thread clientReceiveThread;

	private static TCPTestClient tcp;

	void Awake()
    {
		if (tcp == null)
        {
			tcp = this;
			DontDestroyOnLoad(this);
		}

		ConnectToTcpServer();



	}

    void OnApplicationQuit()
    {
		
		if(socketConnection != null)
        {
			SendMessage();
			socketConnection.GetStream().Close();
			socketConnection.Close();
			clientReceiveThread.Abort();
		}

    }



	public void ConnectToTcpServer()
	{
		try
		{
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}
	private void ListenForData()
	{
		try
		{
			socketConnection = new TcpClient("127.0.0.1", 4444);
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream())
				{
					int length;
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData);
						
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			//Debug.Log("Socket exception: " + socketException);
		}
	}

	public void SendMessage()
	{
		if (socketConnection == null)
		{
			return;
		}
		try
		{
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite)
			{
				string clientMessage = "Shutdown";
				// Convert string message to byte array.                 
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
				Debug.Log("Message sent");
				
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}