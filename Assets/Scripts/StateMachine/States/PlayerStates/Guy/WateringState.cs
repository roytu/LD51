using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityStates.Guy {

public class WateringState : PlayerBaseState
{
    private float t = 0;

    public override void Enter(Player player) {
        t = 0;
        player.animator.SetTrigger("enterWatering");
    }

    public override void Execute(Player player) {
        t += Time.deltaTime;
        if (t > 0.2f) {
            player.stateMachine.ChangeState(new EntityStates.Guy.MainState());
        }
    }

    public override void Exit(Player player) {
        player.animator.SetTrigger("exitWatering");
    }

}

}
