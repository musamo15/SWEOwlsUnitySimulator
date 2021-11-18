using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightMatrix : MonoBehaviour
{

    public Image imageHolder; 

    public Sprite heartImage;
    public Sprite happyImage;
    public Sprite sadImage;
    public Sprite defaultImage;

    private Sprite currentSprite;
    private Sprite lastSprite;


    void Start()
    {
        currentSprite = defaultImage;
        lastSprite = defaultImage;
    }

    public void setImage(string image)
    {
        //Sad, Happy, heart

        image = image.ToLower();
        lastSprite = currentSprite;

        switch (image)
        {
            case "happy":                
                currentSprite = happyImage;
                break;

            case "sad":
                currentSprite = sadImage;
                break;

            case "heart":
                currentSprite = heartImage;
                break;

            default:
                currentSprite = defaultImage;
                break;
    

        }
    }


    public void setNewImage(Sprite image)
    {
        currentSprite = image;
    }


    public void enableImage(Sprite thisImage)
    {
        imageHolder.sprite = thisImage;
    }


    void Update()
    {
        if(lastSprite != currentSprite)
        {
            enableImage(currentSprite);
            lastSprite = currentSprite;  
        }

        
    }
}
