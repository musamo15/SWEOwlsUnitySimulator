using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;
    public GameObject[] ItemImage;
    public int CurrentButtonPressed;
    
    public CalculatePosition calcPos; //new

    public Camera twoDCam;


    private void Update()
    {
        // if need to change to vector3 add Input.mousePosition.z
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

        if (twoDCam.enabled);
        {
            Vector3 worldPosition = twoDCam.ScreenToWorldPoint(screenPosition);

            if (Input.GetMouseButtonDown(0) && ItemButtons[CurrentButtonPressed].Clicked)
            {
                ItemButtons[CurrentButtonPressed].Clicked = false;
                // Quaternion.identity keeps the rotation of the original prefab

                if (CurrentButtonPressed == 1) //element number SpikePrime is set to in levelEditorManager
                {
                    ItemPrefabs[CurrentButtonPressed].transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);
                    Destroy(GameObject.FindGameObjectWithTag("SpikeImage"));
                    //calcPos.Start();
                    calcPos.startingPosition = ItemPrefabs[CurrentButtonPressed].transform.position;
                    calcPos.calculateCurrentPosition();

                }
                else
                {
                    Instantiate(ItemPrefabs[CurrentButtonPressed], new Vector3(worldPosition.x, 0, worldPosition.z), Quaternion.identity);
                    Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
                }

            }
        }
    }
}
