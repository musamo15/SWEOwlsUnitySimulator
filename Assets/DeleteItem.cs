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
        if(Input.GetKey("backspace") || Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
        }

    }

    
}
