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
    public InputActionReference moveKeyboard;
    public InputActionReference interactKeyboard;
    public InputActionReference backKeyboard;
    public InputActionReference powerupKeyboard;

    public InputActionReference moveController;
    public InputActionReference interactController;
    public InputActionReference jumpController;
    public InputActionReference backController;
    public InputActionReference powerupController;

    private void Update()
    {
        _moveDirection = moveKeyboard.action.ReadValue<Vector3>();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = _moveDirection;
    }

    private void OnEnable()
    {
        interactKeyboard.action.started += Interact;
        interactController.action.started += Interact;
    }

    private void OnDisable()
    {
        interactKeyboard.action.started -= Interact;
        interactController.action.started -= Interact;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        Debug.Log("Interacted");
    }
}
