using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingAndLoading : MonoBehaviour
{
    public SceneHandler sceneHandler;

    public void Awake()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
    }

    public void Save()
    {
        sceneHandler.SaveScene();
    }

    public void Load()
    {
        sceneHandler.LoadScene();
    }
}
