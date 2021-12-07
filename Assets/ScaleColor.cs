using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleColor : MonoBehaviour
{
    public int ID;
    private LevelEditorManager editor;
    private int lenCounter = 0;
    private int widthCounter = 0;
    private int lenMax = 100;
    private int lenMin = 0; //A lower value could cause the object's x value scale to reach 0 and disappear
    private int widMax = 100;
    private int widMin = 0; //A lower value could cause the object's z value scale to reach 0 and disappear

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
            if (Input.GetKey("up"))
            {
                lastPressedTime = Time.time;
                if (lenCounter <= lenMax)
                {
                    lenCounter++;
                    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + 10f, 1f, this.gameObject.transform.localScale.z);
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
                if (lenCounter > lenMin)
                {
                    lenCounter--;
                    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x - 10f, 1f, this.gameObject.transform.localScale.z);
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
                    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, 1f, this.gameObject.transform.localScale.z + 10f);
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
                if (widthCounter > widMin)
                {
                    widthCounter--;
                    this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, 1f, this.gameObject.transform.localScale.z - 10f);
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
