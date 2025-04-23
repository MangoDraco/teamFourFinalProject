using KBCore.Refs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class menuRemoval : MonoBehaviour
{
    public GameObject menu;
    
    public Button buttonPress;
    public Button buttonPress2;
    // Start is called before the first frame update
    void Start()
    {
        buttonPress.onClick.AddListener(OnButtonClick);
        buttonPress2.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnButtonClick()
    {
        buttonPress.onClick.RemoveListener(OnButtonClick);
        buttonPress2.onClick.RemoveListener(OnButtonClick);
        menu.SetActive(false);
    }
}
