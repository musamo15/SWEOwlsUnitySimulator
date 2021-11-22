using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShutDownButton : MonoBehaviour
{

    public Button myButton;
    private GameObject shutDownHolder;
    private TCPTestClient shutdownController;



    void Start()
    {


        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(ClickHandler);
    }

   public void ClickHandler()
    {
        shutDownHolder = GameObject.Find("ShutDownHolder");
        shutdownController = shutDownHolder.GetComponent<TCPTestClient>();

         shutdownController.ConnectToTcpServer();

        

    }
    
}
