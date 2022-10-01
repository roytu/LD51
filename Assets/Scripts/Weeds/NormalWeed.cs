using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWeed : Weed
{
    public override void DoAbility() {
        // Do nothing

    }

    public override void SetInitialHealth()
    {
        health = 10;
    }
}
