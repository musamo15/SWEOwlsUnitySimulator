using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItem : MonoBehaviour
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
        if(Input.GetKey("backspace"))
        {
            Destroy(this.gameObject);
            //editor.ItemButtons[ID].quantity++;
            //editor.ItemButtons[ID].quantityText.text = editor.ItemButtons[ID].quantity.ToString();
        }

        //if(Input.GetMouseButtonDown(0))
        //{
        //    this.gameObject.transform.Rotate(0.0f, 10.0f, 0.0f, Space.Self);
        //}
    }

    
}
