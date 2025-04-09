using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerActions;
using static UnityEngine.InputSystem.DefaultInputActions;

public class InputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction EnableMouseControlCamera = delegate { };
    public event UnityAction DisableMouseControlCamera = delegate { };

    PlayerActions inputActions;

    public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();

    void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerActions();
            inputActions.Player.SetCallbacks(instance: this);
        }
        inputActions.Enable();
    }

    void PlayerActions.IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        Move.Invoke(arg0: context.ReadValue<Vector2>());
    }

    void PlayerActions.IPlayerActions.OnRun(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                EnableMouseControlCamera.Invoke();
                break;
            case InputActionPhase.Canceled:
                DisableMouseControlCamera.Invoke();
                break;
        }
    }

    void PlayerActions.IPlayerActions.OnFire(InputAction.CallbackContext context)
    {
        //noop
    }

    void PlayerActions.IPlayerActions.OnInteract(InputAction.CallbackContext context)
    {
        //noop
    }

    void PlayerActions.IPlayerActions.OnJump(InputAction.CallbackContext context)
    {
        //noop
    }

    void PlayerActions.IPlayerActions.OnMenu(InputAction.CallbackContext context)
    {
        //noop
    }

    void PlayerActions.IPlayerActions.OnPowerup(InputAction.CallbackContext context)
    {
        //noop
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}