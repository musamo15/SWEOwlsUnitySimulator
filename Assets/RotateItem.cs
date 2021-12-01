using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{

    public int ID;
    private LevelEditorManager editor;
    private float lastPressedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    void OnMouseOver()
    {
        if (Time.time > lastPressedTime + 0.1f)
        {
            //if (Input.GetMouseButtonDown(1))
            if (Input.GetKey("."))
            {
                lastPressedTime = Time.time;
                this.gameObject.transform.Rotate(0.0f, 10.0f, 0.0f, Space.Self);
            }

            //if (Input.GetMouseButtonDown(0))
            if (Input.GetKey(","))
            {
                lastPressedTime = Time.time;
                this.gameObject.transform.Rotate(0.0f, -10.0f, 0.0f, Space.Self);
            }
        }
    }


}
