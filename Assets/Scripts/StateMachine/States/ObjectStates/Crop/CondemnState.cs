using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityStates.Crop {

public class CondemnState : BaseState<global::Crop>
{
    private float t = 0;

    public CondemnState() {
    }

    public override void Enter(global::Crop cross) {
        //cross.animator.SetTrigger("condemn");
    }
    public override void Execute(global::Crop cross) {
        t += Time.deltaTime;
        if (t > 3f) {
            //cross.Die();
        }
    }
    public override void Exit(global::Crop cross) { 

    }

}

}