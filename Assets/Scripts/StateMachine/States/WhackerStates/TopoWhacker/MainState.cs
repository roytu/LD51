using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityStates.TopoWhacker {

public class MainState : BaseState<Whacker>
{
    private float t = 0;

    public override void Enter(Whacker whacker) {
        t = 0;

        // Spawn projectile
        Vector2 molePosition = new Vector2(whacker.transform.position.x, whacker.transform.position.y);
        Vector2 randomDirection = new Vector2(
            Random.Range(-0.3f, 0.3f),
            Random.Range(-0.3f, 0.3f)
        );
        Vector2 moleDirection = (whacker.GetAimDirection().normalized + randomDirection).normalized;
        float moleSpeed = 18f;

        GameObject projectileGO = GameObject.Instantiate(PrefabsManager.getInstance().topoWhackerProjectilePrefab, whacker.transform.position, Quaternion.identity);
        TopoWhackerProjectile projectile = projectileGO.GetComponent<TopoWhackerProjectile>();
        projectile.direction = moleDirection;
        projectile.speed = moleSpeed;

        // Apply knockback to player
        Player player = GameObject.FindObjectOfType<Player>();
        player.rigidbody.AddForce(-whacker.GetAimDirection().normalized * 300f, ForceMode2D.Impulse);

        CameraManager cameraManager = GameObject.FindObjectOfType<CameraManager>();
        cameraManager.AddScreenshake(0.04f);

    }
    public override void Execute(Whacker whacker) {
        // Animate whacker
        const float PERIOD = 0.3f; 
        float rotAmt = 120f * Mathf.Deg2Rad;
        float rotVal = ((t % PERIOD) - Mathf.Abs((t % PERIOD) - (PERIOD / 2))) / PERIOD * rotAmt;
        whacker.gameObject.transform.localEulerAngles = new Vector3(0, 0, rotVal);

       // Time
        t += Time.fixedDeltaTime;
        if (t > 2f) {
            whacker.stateMachine.ChangeState(new EntityStates.TopoWhacker.IdleState());
        }
    }
    public override void Exit(Whacker whacker) {
        whacker.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

}

}