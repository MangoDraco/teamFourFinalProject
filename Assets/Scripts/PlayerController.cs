using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidBody;

    public float walkSpeed = 5f; //Walking speed
    public float runSpeed = 8f; //Running speed

    public float jumpForce = 7f; //Jump height
    private int jumpCount = 0;
    private bool isGrounded = true;

    private Vector2 _moveDirection; //Vectors for movement in keyboard and xbox DO NOT CHANGE VECTOR TYPE
    private bool _isUsingKeyboard = true; //Changing between xbox and keyboard

    //Tab in order to bring up in-game menu
    public GameObject menu; //Assign this in the inspector!
    private bool isPaused = false; 

    //Keyboard inputs
    public InputActionReference moveKeyboard;
    public InputActionReference interactKeyboard;
    public InputActionReference backKeyboard;
    public InputActionReference powerupKeyboard;
    public InputActionReference jumpKeyboard;

    //Controller inputs
    public InputActionReference moveController;
    public InputActionReference interactController;
    public InputActionReference jumpController;
    public InputActionReference backController;
    public InputActionReference powerupController;

    //Powerup system
    public enum PowerupType { None, CardPlatform}
    private PowerupType currentPowerup = PowerupType.None;

    public GameObject cardPlatformPrefab; //Assign card prefab in inspector!
    public Transform throwPoint; //Empty GameObject at the throw position
    public float throwForce = 5f;

    private void Update()
    {
        DetectInputDevice();

        Vector2 moveInput = _isUsingKeyboard ? moveKeyboard.action.ReadValue<Vector2>() : moveController.action.ReadValue<Vector2>();
        _moveDirection = moveInput;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(_moveDirection.x, rigidBody.velocity.y, _moveDirection.y) * walkSpeed;
        rigidBody.velocity = velocity;
    }

    private void DetectInputDevice()
    {
        if (moveKeyboard.action.WasPerformedThisFrame() || interactKeyboard.action.WasPerformedThisFrame())
        {
            _isUsingKeyboard = true;
        }

        else if (moveController.action.WasPerformedThisFrame() || interactController.action.WasPerformedThisFrame())
        {
            _isUsingKeyboard = false;
        }
    }

    private void OnEnable()
    {
        interactKeyboard.action.started += Interact;
        interactController.action.started += Interact;

        backKeyboard.action.started += ToggleMenu;
        backController.action.started += ToggleMenu;

        powerupKeyboard.action.started += ActivatePowerup;
        powerupController.action.started += ActivatePowerup;

        jumpKeyboard.action.started += Jump;
        jumpController.action.started += Jump;
    }

    private void OnDisable()
    {
        interactKeyboard.action.started -= Interact;
        interactController.action.started -= Interact;

        backKeyboard.action.started -= ToggleMenu;
        backController.action.started -= ToggleMenu;

        powerupKeyboard.action.started -= ActivatePowerup;
        powerupController.action.started -= ActivatePowerup;

        jumpKeyboard.action.started -= Jump;
        jumpController.action.started -= Jump;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        Debug.Log("Interacted");
    }

    private void ToggleMenu(InputAction.CallbackContext obj)
    {
        isPaused = !isPaused;
        menu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1; //Pause/Unpause game
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }

    public void PickupPowerup(PowerupType newPowerup)
    {
        currentPowerup = newPowerup;
        Debug.Log("Picked up powerup: " + newPowerup);
    }

    private void UsePowerup(InputAction.CallbackContext obj)
    {
        if (currentPowerup == PowerupType.CardPlatform)
        {
            ThrowCardPlatform();
        }
    }

    private void ActivatePowerup(InputAction.CallbackContext obj)
    {
        if (currentPowerup == PowerupType.None)
        {
            Debug.Log("No powerup available");
            return;
        }

        switch (currentPowerup)
        {
            case PowerupType.CardPlatform:
                ThrowCardPlatform();
                break;

            //Future powerups here
            default:
                Debug.Log("Unknown powerup");
                break;
        }

        currentPowerup = PowerupType.None;
    }

    private void ThrowCardPlatform()
    {
        GameObject card = Instantiate(cardPlatformPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody cardRigidbody = card.GetComponent<Rigidbody>();
        if (cardRigidbody != null )
        {
            cardRigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }

        currentPowerup = PowerupType.None;
        Debug.Log("Card platform thrown");
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (isGrounded || jumpCount < 1) //Allows for second jump in air
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
            isGrounded = false;
            Debug.Log(jumpCount == 1 ? "Jump" : "Double Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //Need to tag plane as ground!
        {
            isGrounded = true;
            jumpCount = 0; //Reset jump count after landing
        }
    }
}
