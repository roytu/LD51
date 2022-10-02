using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityStates.NormalWhacker {

public class MainState : BaseState<Whacker>
{
    float t = 0;

    public override void Enter(Whacker whacker) {
        t = 0;

        // Spawn a basic hitbox
        GameObject hitboxGO = GameObject.Instantiate(PrefabsManager.getInstance().normalWhackerHitboxPrefab, whacker.transform.position, Quaternion.identity);
        Hitbox hitbox = hitboxGO.GetComponent<Hitbox>();
        float r = Random.Range(0, 360) * Mathf.Deg2Rad;
        hitbox.knockbackDirection = new Vector2(
            Mathf.Cos(r),
            Mathf.Sin(r)
        );
    }
    public override void Execute(Whacker whacker) {
        const float PERIOD = 0.3f; 
        float rotAmt = 120f * Mathf.Deg2Rad;
        float rotVal = ((t % PERIOD) - Mathf.Abs((t % PERIOD) - (PERIOD / 2))) / PERIOD * rotAmt;

        whacker.gameObject.transform.localEulerAngles = new Vector3(0, 0, rotVal);
        t += Time.deltaTime;
        if (t > 0.5f) {
            whacker.stateMachine.ChangeState(new EntityStates.NormalWhacker.IdleState());
        }
    }
    public override void Exit(Whacker whacker) {
        whacker.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
    }


}

}