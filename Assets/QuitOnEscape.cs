using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitOnEscape : MonoBehaviour
{
    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void Update()
    {
        if (inputActions.Player.Escape.WasPressedThisFrame()) {
            Application.Quit();
        }
    }
}
