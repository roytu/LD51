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
        global::NormalWhacker normalWhacker = (global::NormalWhacker)whacker;
        Vector3 position = normalWhacker.transform.position;
        GameObject.Instantiate(normalWhacker.normalHitboxGO, position, Quaternion.identity);
    }
    public override void Execute(Whacker whacker) {
        const float PERIOD = 0.3f; 
        float rotAmt = 120f * Mathf.PI / 2;
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