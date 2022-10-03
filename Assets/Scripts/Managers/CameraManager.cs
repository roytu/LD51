using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private GameObject followTarget;
    public Vector2 zoomTargetPosition;
    public float zoomTime;

    private Vector3 origDelta;
    private float origScale;

    public PlayerInputActions playerInputActions;
    private InputAction lookAction;

    private float screenshakeAmt = 0;
    private Camera camera;

    void Awake() {
        playerInputActions = new PlayerInputActions();
        camera = GetComponent<Camera>();
    }

    void Start() {
        origDelta = transform.position;
        origScale = GetComponent<Camera>().orthographicSize;
    }

    public void SetFollowTarget(GameObject target) {
        followTarget = target;
    }

    public void SetZoomTarget(Vector2 zoomPosition) {
        zoomTargetPosition = zoomPosition;
        zoomTime = 0;
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
            followPosition = (mousePos + followPosition * 1) / 2f;

            // Precalculate zoom target params

            // zoomInfluence is a number between 0 and 1
            // 1 means it takes over the camera basically
            float zoomInfluence = Logistic(zoomTime);
            float zoomScale = Logistic(zoomTime) / 0.5f + 0.5f;

            Vector2 combinedPosition = Lerp(followPosition, zoomTargetPosition, zoomInfluence);
            //followPosition = (followPosition * 7f + combinedPosition) / 8f;
            followPosition = combinedPosition;

            // Add screenshake
            screenshakeAmt *= 0.95f;
            followPosition += new Vector2(
                Random.Range(-screenshakeAmt / 2, screenshakeAmt / 2),
                Random.Range(-screenshakeAmt / 2, screenshakeAmt / 2)
            );

            // Assign new position and scale
            transform.position = new Vector3(followPosition.x, followPosition.y, 0) + origDelta;

            float targetZoom = Lerp(origScale, zoomScale, zoomInfluence);
            camera.orthographicSize = (camera.orthographicSize * 7 + targetZoom) / 8f;
        }

        zoomTime += Time.deltaTime;
    }

    private float Logistic(float x, float x0=0.3f, float k=3) {
        float denom = 1 + Mathf.Exp(-k * (x - x0));
        return 1 - (1 / denom);
    }

    private float Lerp(float x, float y, float amt) {
        return x * (1 - amt) + y * amt;
    }

    private Vector2 Lerp(Vector2 x, Vector2 y, float amt) {
        return x * (1 - amt) + y * amt;
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
