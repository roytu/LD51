using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntityStates.NormalWeed {

public class MitosisState : BaseState<global::Weed>
{
    float t = 0;

    public override void Enter(global::Weed weed) {
        weed.animator.SetTrigger("enterMitosis");
    }
    public override void Execute(global::Weed weed) {
        if (t > 0.8f) {
            stateMachine.ChangeState(new EntityStates.NormalWeed.MainState());
        }
        t += Time.deltaTime;
    }
    public override void Exit(global::Weed weed) {
        // Spawn another weed next to you
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        Vector3 newPosition = weed.transform.position + new Vector3(0.5f, 0, 0);
        GameObject.Instantiate(gameManager.normalWeedPrefab, newPosition, Quaternion.identity);
        weed.animator.SetTrigger("exitMitosis");
    }


}

}