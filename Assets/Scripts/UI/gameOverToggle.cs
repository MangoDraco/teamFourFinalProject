using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverToggle : MonoBehaviour
{
    public GameObject button;
    public Button buttonPress;
    public bool bgFlag;
    public HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
        if (healthManager.dead == true)
        {
            Debug.Log("I am showing the button");
            bgFlag = true;
            button.SetActive(true);
            buttonPress.onClick.AddListener(OnButtonClick);
        }
    }
    void OnButtonClick()
    {
        buttonPress.onClick.RemoveListener(OnButtonClick);
        button.SetActive(false);
        bgFlag = false;
    }
}
