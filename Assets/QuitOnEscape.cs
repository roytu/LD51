using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitOnEscape : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private InputAction escapeAction;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        escapeAction = inputActions.Player.Escape;
        escapeAction.performed += _ => Application.Quit();
    }

    void OnEnable() {
        escapeAction.Enable();
    }

    void OnDisable() {
        escapeAction.Disable();
    }



}
