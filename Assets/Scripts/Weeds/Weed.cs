using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weed : MonoBehaviour
{
    public int health = 0;

    private float whiteTimer = 0f;
    private new SpriteRenderer renderer;

    public StateMachine<Weed> stateMachine;
    [NonSerialized]
    public Animator animator;
    [NonSerialized]
    public Rigidbody2D rigidbody;

    protected void Awake() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine<Weed>();
        stateMachine.Awake();
        // The inheriting weed is responsible for running stateMachine.Configure(new MainState()) here.

    }

    void FixedUpdate() {
        stateMachine.Update();
    }


    void Start() {
        SetInitialHealth();
        renderer = GetComponent<SpriteRenderer>();
    }

    public virtual void DoAbility() {
    }

    private void TakeDamage(int damage) {
        whiteTimer = 0.05f;
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void Update() {
        if (whiteTimer > 0) {
            whiteTimer -= Time.deltaTime;
            renderer.color = Color.white;
        } else {
            renderer.color = new Color32(0xD4, 0x4C, 0x44, 0xFF);
        }

    }

    public void Hit(int damage) {
        TakeDamage(damage);
    }

    public void Die() {
        CameraManager cameraManager = FindObjectOfType<CameraManager>();
        cameraManager.SetZoomTarget(transform.position);

        Destroy(gameObject);
    }

    public virtual void OnNewWave() {}

    public abstract void SetInitialHealth();
}
