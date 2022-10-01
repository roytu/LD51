using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : BaseState<Player>
{
    public virtual bool DoPrimary(Player player) { 
        return false;
    }
    public virtual bool DoSecondary(Player player) {
        return false;
    }
    public virtual bool DoUtility(Player player) {
        return false;
    }
    public virtual bool DoSpecial(Player player) {
        return false;
    }

}
