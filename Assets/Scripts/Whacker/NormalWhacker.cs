using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class NormalWhacker : Whacker
{

    private List<AsyncOperationHandle> asyncOperationHandles;
    public GameObject normalHitboxGO;

    protected new void Awake() {
        base.Awake();

        // Load prefabs
        asyncOperationHandles = new List<AsyncOperationHandle>();
        normalHitboxGO = LoadPrefab("Assets/Prefabs/Hitboxes/NormalWhackerHitbox.prefab");

        // Configure state machine
        stateMachine.Configure(this, new EntityStates.NormalWhacker.IdleState());
    }

    GameObject LoadPrefab(object key) {
        var handle = Addressables.LoadAssetAsync<GameObject>(key);
        asyncOperationHandles.Add(handle);
        return handle.WaitForCompletion();
    }


    public override void Whack()
    {
        base.Whack();
        stateMachine.Configure(this, new EntityStates.NormalWhacker.MainState());
    }


    void OnDisable() {
        foreach (var asyncOperationHandle in asyncOperationHandles) {
            Addressables.Release(asyncOperationHandle);
        }
    }

}
