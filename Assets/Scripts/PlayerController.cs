using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidBody;

    public float walkSpeed;
    public float runSpeed;

    private Vector3 _moveDirection;
    public InputActionReference move;
    public InputActionReference interact;

    private void Update()
    {
        _moveDirection = move.action.ReadValue<Vector3>();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = _moveDirection;
    }

    private void OnEnable()
    {
        interact.action.started += Interact;
    }

    private void OnDisable()
    {
        interact.action.started -= Interact;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        Debug.Log("Interacted");
    }
}
