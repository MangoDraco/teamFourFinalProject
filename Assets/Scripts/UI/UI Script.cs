using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject keyGotText;
    // Start is called before the first frame update
    private void Start()
    {
        PowerIcon iconchange = GetComponentInChildren<PowerIcon>();
        keyGotText.SetActive(false);
    }

    public void winText()
    {
        keyGotText.SetActive(true);
    }
}
