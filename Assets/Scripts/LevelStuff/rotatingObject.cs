using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingObject : MonoBehaviour
{
    public Vector3 rotation;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotation * speed * Time.deltaTime);
    }
}
