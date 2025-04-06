using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 10f;

    Vector2 moveInput;
    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Run();
    }

    void Run()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * walkSpeed, rigidbody.velocity.y, moveInput.y * walkSpeed);
        rigidbody.velocity = transform.TransformDirection(playerVelocity);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}