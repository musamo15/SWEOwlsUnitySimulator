using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    void Start()
    {
        if (this.gameObject.tag == "SpikeImage")
        {
            transform.Rotate(-90.0f, 0.0f, -90.0f, Space.Self);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);
    }
}
