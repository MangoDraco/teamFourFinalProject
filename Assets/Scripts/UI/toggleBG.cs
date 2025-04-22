using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleBG : MonoBehaviour
{

    public gameOverToggle button;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (button.bgFlag == true)
        {
            
            image.gameObject.SetActive(true);
        }
        else
        {
            image.gameObject.SetActive(false);
        }
    }
}
