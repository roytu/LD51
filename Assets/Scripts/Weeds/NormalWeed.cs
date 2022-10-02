using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWeed : Weed
{

    protected new void Awake() {
        base.Awake();

        stateMachine.Configure(this, new EntityStates.NormalWeed.MainState());
    }

    public override void DoAbility() {
        // Do nothing

    }

    public override void SetInitialHealth()
    {
        health = 10;
    }

    public override void OnNewWave() {
        base.OnNewWave();

        stateMachine.Configure(this, new EntityStates.NormalWeed.MitosisState());
    }
}
