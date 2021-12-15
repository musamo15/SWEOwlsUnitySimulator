using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int ID;
    public bool Clicked = false;
    private LevelEditorManager editor;

    public Camera twoDCam;

    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    public void ButtonClicked()
    {
        if (twoDCam.enabled)
        {


            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Vector3 worldPosition = twoDCam.ScreenToWorldPoint(screenPosition);

            Clicked = true;
            Instantiate(editor.ItemImage[ID], new Vector3(worldPosition.x, worldPosition.y, worldPosition.z), Quaternion.identity);
            editor.CurrentButtonPressed = ID;
         
        }
    }
}
