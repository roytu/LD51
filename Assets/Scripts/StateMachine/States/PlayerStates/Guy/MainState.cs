using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityStates.Guy {

public class MainState : GenericMainState
{
    public override bool DoPrimary(Player player) {
        base.DoPrimary(player);

        // TODO Check for cooldowns and stuff
        stateMachine.ChangeState(new EntityStates.Guy.WhackingState());
        return true;
    }

    public override bool DoSecondary(Player player) {
        base.DoSecondary(player);

        // TODO Check for cooldowns and stuff
        return true;
    }

    public override bool DoUtility(Player player) {
        base.DoUtility(player);

        // TODO Check for cooldowns and stuff
        return true;
    }

    public override bool DoSpecial(Player player) {
        base.DoSpecial(player);

        // TODO Check for cooldowns and stuff
        return true;
    }


}

}