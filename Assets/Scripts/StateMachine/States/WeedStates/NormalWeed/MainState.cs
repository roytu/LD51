using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityStates.NormalWeed {

public class MainState : BaseState<global::Weed>
{

    public Vector3 target;
    private float maxVelocity = 1f;
    private float walkForce = 10f;
    private float timeTillRetarget = 0f;

    public override void Enter(global::Weed weed) {
        weed.animator.SetTrigger("enterWalk");

        // Pick a random location to walk to
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        target = gameManager.GetRandomLocation();

        Retarget();
    }

    public override void Execute(global::Weed weed) {
        // Move towards target
        Vector3 delta = target - weed.transform.position;
        delta.z = 0;
        delta.Normalize();
        Vector3 force = delta * walkForce;
        weed.rigidbody.AddForce(force, ForceMode2D.Force);
        if (weed.rigidbody.velocity.magnitude > maxVelocity) {
            // Soft relax max velocity to allow for large impulses
            Vector2 clampedVelocity = weed.rigidbody.velocity.normalized * maxVelocity;
            weed.rigidbody.velocity = (clampedVelocity + weed.rigidbody.velocity * 3f) / 4f;
        }

        timeTillRetarget -= Time.deltaTime;
        if (timeTillRetarget <= 0) {
            Retarget();
        }
    }

    private void Retarget() {
        timeTillRetarget = Random.Range(3f, 5f);
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        target = gameManager.GetRandomLocation();
    }


    public override void Exit(global::Weed weed) {
        weed.animator.SetTrigger("exitWalk");
    }


}

}