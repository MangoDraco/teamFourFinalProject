using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerIcon : MonoBehaviour
{
    public Image powerIcon;
    public Sprite hatBlink;
    public Sprite cardPlatform;
    public int iconVal;
    public void changeIcon(int iconVal)
    {
        switch (iconVal)
        {
            case 1:
                powerIcon.sprite = hatBlink;
                break;
            case 2:
                powerIcon.sprite = cardPlatform;
                break;
            default:
                Debug.Log("No powerup");
                break;
        }
    }


}
