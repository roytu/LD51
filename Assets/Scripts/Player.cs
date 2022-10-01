using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInputActions playerInputActions;

    [NonSerialized]
    public InputAction move;

    [NonSerialized]
    public InputAction abilityPrimary;
    [NonSerialized]
    public InputAction abilitySecondary;
    [NonSerialized]
    public InputAction abilityUtility;
    [NonSerialized]
    public InputAction abilitySpecial;

    private Rigidbody2D rigidbody;

    public float walkSpeed;
    public float maxWalkSpeed;

    public StateMachine<Player> stateMachine;

    public event Action<Collision> OnCollisionEnterAction;

    private Whacker whacker;
    private GameObject whackerHolder;

    [NonSerialized]
    public Animator animator;

    protected void Awake() {
        animator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        rigidbody = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine<Player>();
        stateMachine.Awake();
        // The inheriting player is responsible for running stateMachine.Configure(new MainState()) here.
        stateMachine.Configure(this, new EntityStates.Guy.MainState());

        whackerHolder = transform.Find("WhackerHolder").gameObject;

    }

    protected void Start() {
        CameraManager cameraManager = FindObjectOfType<CameraManager>();
        if (cameraManager) {
            cameraManager.SetFollowTarget(gameObject);
        }

    }

    private void OnEnable() {
        move = playerInputActions.Player.Move;
        move.Enable();

        abilityPrimary = playerInputActions.Player.AbilityPrimary;
        abilityPrimary.Enable();
        abilitySecondary = playerInputActions.Player.AbilitySecondary;
        abilitySecondary.Enable();
        abilityUtility = playerInputActions.Player.AbilityUtility;
        abilityUtility.Enable();
        abilitySpecial = playerInputActions.Player.AbilitySpecial;
        abilitySpecial.Enable();
    }

    private void OnDisable() {
        move.Disable();
        abilityPrimary.Disable();
    }

    void FixedUpdate() {
        stateMachine.Update();

        /*
        // Handle movement
        Vector2 moveVector = GetMoveVector();
        Vector2 force = moveVector * walkSpeed;

        rb.AddForce(force);
        if (rb.velocity.magnitude > maxWalkSpeed) {
            rb.velocity = rb.velocity.normalized * maxWalkSpeed;
        }
        */
    }

    public Vector2 GetMoveVector()
    {
        if (move == null) {
            return Vector2.zero;
        }
        return move.ReadValue<Vector2>();
    }

    public bool IsPrimaryPressed()
    {
        if (abilityPrimary == null) {
            return false;
        }
        return abilityPrimary.IsPressed();
    }

    public bool IsSecondaryPressed()
    {
        if (abilitySecondary == null) {
            return false;
        }
        return abilitySecondary.IsPressed();
    }

    public bool IsUtilityPressed()
    {
        if (abilityUtility == null) {
            return false;
        }
        return abilityUtility.IsPressed();
    }

    public bool IsSpecialPressed()
    {
        if (abilitySpecial == null) {
            return false;
        }
        return abilitySpecial.IsPressed();
    }

    public void Whack() {
        if (whacker == null) {
            return;
        }

        whacker.Whack();
    }

    public void SetWhacker(Whacker whacker) {
        this.whacker = whacker;
        whacker.transform.SetParent(whackerHolder.transform);
        whacker.transform.localPosition = Vector3.zero;
        whacker.transform.localEulerAngles = Vector3.zero;
        whacker.transform.localScale = Vector3.one;
    }

}
