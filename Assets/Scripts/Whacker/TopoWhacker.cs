using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TopoWhacker : Whacker
{
    protected new void Awake() {
        base.Awake();

        // Configure state machine
        stateMachine.Configure(this, new EntityStates.TopoWhacker.IdleState());
    }

    public override void Whack()
    {
        base.Whack();
        stateMachine.Configure(this, new EntityStates.TopoWhacker.MainState());
    }

}
