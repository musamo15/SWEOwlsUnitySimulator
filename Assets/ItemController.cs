using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class ItemController : MonoBehaviour
{
    public int ID;
    //public int quantity;
    //public TextMeshProUGUI quantityText;
    public bool Clicked = false;
    private LevelEditorManager editor;

    public Camera twoDCam;

    // Start is called before the first frame update
    void Start()
    {
        //quantityText.text = quantity.ToString();
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    public void ButtonClicked()
    {
        //if(quantity > 0)
        //{
        if (twoDCam.enabled)
            {


            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Vector3 worldPosition = twoDCam.ScreenToWorldPoint(screenPosition);

            Clicked = true;
            Instantiate(editor.ItemImage[ID], new Vector3(worldPosition.x, worldPosition.y, worldPosition.z), Quaternion.identity);
            //quantity--;
            //quantityText.text = quantity.ToString();
            editor.CurrentButtonPressed = ID;
            //}
        }
    }
}
