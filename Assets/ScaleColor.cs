using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleColor : MonoBehaviour
{
    public int ID;
    private LevelEditorManager editor;
    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    void OnMouseOver()
    {

        if (Input.GetKey("1") || Input.GetKey("[1]"))
        {
            this.gameObject.transform.localScale = new Vector3(10f, 1f, 10);
        }
        else if (Input.GetKey("2") || Input.GetKey("[2]"))
        {
            this.gameObject.transform.localScale = new Vector3(20f, 2f, 20);
        }
        else if (Input.GetKey("3") || Input.GetKey("[3]"))
        {
            this.gameObject.transform.localScale = new Vector3(30f, 3f, 30);
        }
        else if (Input.GetKey("4") || Input.GetKey("[4]"))
        {
            this.gameObject.transform.localScale = new Vector3(40f, 1f, 40);
        }
        else if (Input.GetKey("5") || Input.GetKey("[5]"))
        {
            this.gameObject.transform.localScale = new Vector3(50f, 1f, 50);
        }
        else if (Input.GetKey("6") || Input.GetKey("[6]"))
        {
            this.gameObject.transform.localScale = new Vector3(100f, 1f, 10);
        }
        else if (Input.GetKey("7") || Input.GetKey("[7]"))
        {
            this.gameObject.transform.localScale = new Vector3(200f, 1f, 10);
        }
        else if (Input.GetKey("8") || Input.GetKey("[8]"))
        {
            this.gameObject.transform.localScale = new Vector3(300f, 1f, 10);
        }
        else if (Input.GetKey("9") || Input.GetKey("[9]"))
        {
            this.gameObject.transform.localScale = new Vector3(400f, 1f, 5);
        }
        else if (Input.GetKey("0") || Input.GetKey("[0]"))
        {
            this.gameObject.transform.localScale = new Vector3(500f, 1f, 10);
        }

        //else
        //{
        //    this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        //}
    }
}
