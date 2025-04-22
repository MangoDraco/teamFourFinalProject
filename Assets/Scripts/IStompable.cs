using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStompable
{
    //Implement on child game object of enemy. Child game object will have a box collider that is on top of their head in order to kill them but if they just run into them normally then player will take damage and enemy will not die
    void Die();
    void OnStomped(); //For VFX
}
