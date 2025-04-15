using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackingsystem : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject m_target = null;
    Vector3 m_LastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;
    bool CanSee;

    // Update is called once per frame
    void Update()
    {
        if (CanSee == true)
        {
            if (m_target)
            {
                if (m_LastKnownPosition != m_target.transform.position)
                {
                    m_LastKnownPosition = m_target.transform.position;
                    m_lookAtRotation = Quaternion.LookRotation(m_LastKnownPosition - transform.position);
                }

                if (transform.rotation != m_lookAtRotation)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Cotton")
        {
            Debug.Log("Enemy can see player");
            CanSee = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Cotton")
        {
            Debug.Log("Enemy cannot see player");
            CanSee = false;
        }
    }

    bool SetTarget(GameObject target)
    {
        if (target)
        {
            return false;
        }

        m_target = target;

        return true;
    }
}
