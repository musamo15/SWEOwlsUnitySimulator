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
	string ip = "127.0.0.1";
	int port = 4444;

	public static TCPTestClient tcp;

	void Awake()
    {
		if (tcp == null)
        {
			tcp = this;
			DontDestroyOnLoad(this);
			
		}
		else
        {
			DestroyObject(gameObject);
        }


	}

    void OnApplicationQuit()
    {
		try
        {
			ConnectToTcpServer();
		}
		catch(Exception e)
        {

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
			Debug.Log("Could not connect to server " + e);
		}
	}



	private void ConnectCallback(IAsyncResult ar)
	{
		try
		{
			// Retrieve the socket from the state object.  
			TcpClient client = (TcpClient)ar.AsyncState;

			SendMessage();
			
			// Complete the connection.  
			client.EndConnect(ar);
			client.Close();

		}
		catch (Exception e)
		{
			Console.WriteLine(e.ToString());
		}
	}

	private void ListenForData()
	{
		try
		{
			socketConnection = new TcpClient(AddressFamily.InterNetwork);
			socketConnection.BeginConnect(ip, port,
				new AsyncCallback(ConnectCallback), socketConnection);

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
				//Debug.Log("Shutdown message sent");
				
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}