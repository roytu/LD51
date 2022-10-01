using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GenericMainState : PlayerBaseState
{

    private const float WALK_SPEED = 1000f;
    private const float MAX_WALK_SPEED = 10f;

    public GenericMainState() {
    }

    void HandleInput(Player player) {
        HandleMovement(player);
        HandleAbilities(player);
    }

    void HandleMovement(Player player) {
        // Get input
        Vector2 moveDirection = player.GetMoveVector();

        // Flip h
        float lookDir = 1;
        if (Mathf.Abs(moveDirection.x) > 0.1f) {
            if (moveDirection.x < 0) {
                lookDir = 1;
            } else if (moveDirection.x > 0) {
                lookDir = -1;
            }
            player.transform.localScale = new Vector3(Mathf.Abs(player.transform.localScale.x) * lookDir, player.transform.localScale.y, player.transform.localScale.z);
        }

        // Apply impulse
        // TODO do we need Time.fixedDeltaTime here?
        Vector2 moveForce = moveDirection.normalized * WALK_SPEED;
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(moveForce);

        if (rigidbody.velocity.magnitude > MAX_WALK_SPEED) {
            rigidbody.velocity = rigidbody.velocity.normalized * MAX_WALK_SPEED;
        }

        // Update animator
        player.animator.SetFloat("velocity", rigidbody.velocity.magnitude);
    }

    void HandleAbilities(Player player) {
        if (player.IsPrimaryPressed()) {
            DoPrimary(player);
            return;
        }

        if (player.IsSecondaryPressed()) {
            DoSecondary(player);
            return;
        }

        if (player.IsUtilityPressed()) {
            DoUtility(player);
            return;
        }

        if (player.IsSpecialPressed()) {
            DoSpecial(player);
            return;
        }
    }

    public override void Enter(Player player) {
    }
    public override void Execute(Player player) {
        HandleInput(player);
    }
    public override void Exit(Player player) { }

}
