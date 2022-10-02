using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Whacker : MonoBehaviour
{
    public StateMachine<Whacker> stateMachine;

    [NonSerialized]
    public Animator animator;

    public virtual void Whack() { }

    protected void Awake() {
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine<Whacker>();
        stateMachine.Awake();
        // The inheriting whacker is responsible for running stateMachine.Configure(new MainState()) here.

    }

    void FixedUpdate() {
        stateMachine.Update();
    }

    public Vector2 GetAimDirection() {
        CameraManager cameraManager = FindObjectOfType<CameraManager>();
        Vector2 myPos = new Vector2(
            stateMachine.owner.transform.position.x,
            stateMachine.owner.transform.position.y
        );
        return (cameraManager.GetMousePos() - myPos);
    }


}
