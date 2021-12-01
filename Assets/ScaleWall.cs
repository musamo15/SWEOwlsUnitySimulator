using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScaleWall : MonoBehaviour
{
    public int ID;
    private LevelEditorManager editor;
    // Start is called before the first frame update
    private int lenCounter = 0;
    private int widthCounter = 0;
    private int lenMax = 10;
    private int lenMin = -6; //A lower value could cause the object's x value scale to reach 0 and disappear
    private int widMax = 10;
    private int widMin = -2; //A lower value could cause the object's z value scale to reach 0 and disappear
    private float lastPressedTime = 0f;

    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    void OnMouseOver()
    {

        if (Input.GetKey("1") || Input.GetKey("[1]"))
        {
            this.gameObject.transform.localScale = new Vector3(100f, 80f, 10);
        }
        else if (Input.GetKey("2") || Input.GetKey("[2]"))
        {
            this.gameObject.transform.localScale = new Vector3(200f, 80f, 20);
        }
        else if (Input.GetKey("3") || Input.GetKey("[3]"))
        {
            this.gameObject.transform.localScale = new Vector3(300f, 80f, 20);
        }
        else if (Input.GetKey("4") || Input.GetKey("[4]"))
        {
            this.gameObject.transform.localScale = new Vector3(400f, 80f, 10);
        }
        else if (Input.GetKey("5") || Input.GetKey("[5]"))
        {
            this.gameObject.transform.localScale = new Vector3(500f, 80f, 10);
        }
        else if (Input.GetKey("6") || Input.GetKey("[6]"))
        {
            this.gameObject.transform.localScale = new Vector3(500f, 80f, 5);
        }
        else if (Input.GetKey("7") || Input.GetKey("[7]"))
        {
            this.gameObject.transform.localScale = new Vector3(400f, 80f, 5);
        }
        else if (Input.GetKey("8") || Input.GetKey("[8]"))
        {
            this.gameObject.transform.localScale = new Vector3(300f, 80f, 5);
        }
        else if (Input.GetKey("9") || Input.GetKey("[9]"))
        {
            this.gameObject.transform.localScale = new Vector3(200f, 80f, 5);
        }
        else if (Input.GetKey("0") || Input.GetKey("[0]"))
        {
            this.gameObject.transform.localScale = new Vector3(400f, 80f, 20);
        }
        //else if continue with as many different scales we want
        //{
        //    this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        //}
        if (Time.time > lastPressedTime + 0.1f)
        {
            if (Input.GetKey("up"))
            {
                lastPressedTime = Time.time;
                if (lenCounter <= lenMax)
                {
                    lenCounter++;
                    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + 50, 80f, this.gameObject.transform.localScale.z);
                }
                //else
                //{
                //    lenCounter = 1;
                //    this.gameObject.transform.localScale = new Vector3(50f, 50f, this.gameObject.transform.localScale.z);
                //}
            }

            if (Input.GetKey("down"))
            {
                lastPressedTime = Time.time;
                if (lenCounter >= lenMin)
                {
                    lenCounter--;
                    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x - 50, 80f, this.gameObject.transform.localScale.z);
                }
                //else
                //{
                //    lenCounter = 1;
                //    this.gameObject.transform.localScale = new Vector3(50f, 50f, this.gameObject.transform.localScale.z);
                //}
            }

            if (Input.GetKey("right"))
            {
                lastPressedTime = Time.time;
                if (widthCounter <= widMax)
                {
                    widthCounter++;
                    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, 80f, this.gameObject.transform.localScale.z + 5);
                }
                //else
                //{
                //    widthCounter = 1;
                //    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, 50f, 5);
                //}
            }

            if (Input.GetKey("left"))
            {
                lastPressedTime = Time.time;
                if (widthCounter >= widMin)
                {
                    widthCounter--;
                    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, 80f, this.gameObject.transform.localScale.z - 5);
                }
                //else
                //{
                //    widthCounter = 1;
                //    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, 50f, 5);
                //}
            }
        }
    }
}
