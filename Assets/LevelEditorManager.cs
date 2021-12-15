using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    public ItemController[] ItemButtons;
    public GameObject[] ItemPrefabs;
    public GameObject[] characterButtons;
    public GameObject[] ItemImage;
    public int CurrentButtonPressed;
    
    public CalculatePosition calcPos;

    public Camera twoDCam;

    public GameObject spikePrimeObject;
    public TwoMotorControl motorController;

    public void Start()
    {
        spikePrimeObject = GameObject.FindGameObjectWithTag("SpikePrime");
    }


    private void Update()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

        if (twoDCam.enabled == true);
        {
            Vector3 worldPosition = twoDCam.ScreenToWorldPoint(screenPosition);

            if (Input.GetMouseButtonDown(0) && ItemButtons[CurrentButtonPressed].Clicked)
            {
                ItemButtons[CurrentButtonPressed].Clicked = false;
                // Quaternion.identity keeps the rotation of the original prefab

                if (CurrentButtonPressed == 1) //element number SpikePrime is set to in levelEditorManager
                {
                    ItemPrefabs[CurrentButtonPressed].transform.position = new Vector3(worldPosition.x, 10, worldPosition.z);
                    ItemPrefabs[CurrentButtonPressed].transform.rotation = GameObject.FindWithTag("SpikeImage").transform.rotation;
                    Destroy(GameObject.FindWithTag("SpikeImage"));
                    calcPos.startingPosition = ItemPrefabs[CurrentButtonPressed].transform.position;
                    calcPos.calculateCurrentPosition();

                    motorController = spikePrimeObject.GetComponent<TwoMotorControl>();
                    motorController.setIsStalled(false);

                }

                else
                {
                    GameObject newObject = Instantiate(ItemPrefabs[CurrentButtonPressed], new Vector3(worldPosition.x, 0, worldPosition.z), GameObject.FindWithTag("ItemImage").transform.rotation) as GameObject;
                    newObject.transform.localScale = GameObject.FindWithTag("ItemImage").transform.localScale;
                    Destroy(GameObject.FindWithTag("ItemImage"));
                }

            }
        }
    }
}
