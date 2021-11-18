using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementTest : MonoBehaviour
{

    public float speed = 100f;
    public Text txt;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        
        if(Input.GetKey("w"))
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;



    }
}
