using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Whacker : MonoBehaviour
{
    public StateMachine<Whacker> stateMachine;

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


}
