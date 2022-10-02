using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private GameObject followTarget;

    private Vector3 origDelta;

    public PlayerInputActions playerInputActions;
    private InputAction lookAction;

    private float screenshakeAmt = 0;

    void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    void Start() {
        origDelta = transform.position;
    }

    public void SetFollowTarget(GameObject target) {
        followTarget = target;
    }

    void Update()
    {
        if (followTarget) {
            // Get target position
            Vector2 followPosition = new Vector2(
                followTarget.transform.position.x,
                followTarget.transform.position.y
            );

            // Get lookahead
            Vector2 mousePos = GetMousePos();
            followPosition = (mousePos + followPosition * 3) / 4f;

            // Add screenshake
            screenshakeAmt *= 0.95f;
            followPosition += new Vector2(
                Random.Range(-screenshakeAmt / 2, screenshakeAmt / 2),
                Random.Range(-screenshakeAmt / 2, screenshakeAmt / 2)
            );

            // Assign new position
            transform.position = new Vector3(followPosition.x, followPosition.y, 0) + origDelta;
        }
    }

    public Vector2 GetMousePos() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(
            lookAction.ReadValue<Vector2>()
        );
        return new Vector2(mousePos.x, mousePos.y);
    }

    public void AddScreenshake(float amount) {
        screenshakeAmt += amount;
    }

    private void OnEnable() {
        lookAction = playerInputActions.Player.Look;
        lookAction.Enable();
    }

    private void OnDisable() {
        lookAction.Disable();
    }

}
