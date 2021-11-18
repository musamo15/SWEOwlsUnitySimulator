using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class HideToolBar: MonoBehaviour
{
    public Button toolButton;
    public GameObject toolBar;

    public void Start()
    {
        Button btn = toolButton.GetComponent<Button>();
        btn.onClick.AddListener(ShowBar);
    }

    public void ShowBar()
    {
        toolBar.SetActive(false);
    }

}
