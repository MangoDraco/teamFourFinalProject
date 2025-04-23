using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtboxTimer : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
    public float targetTime = 5.0f;
    public float maxTime = 5.0f;

     void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    public void ResetTimer()
    {
        healthManager.invinc = true;
        targetTime = maxTime;
    }

    void timerEnded()
    {
        healthManager.invinc = false;
    }
}
