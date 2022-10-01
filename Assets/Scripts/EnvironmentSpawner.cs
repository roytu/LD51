using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnvironmentSpawner : MonoBehaviour
{

    private List<AsyncOperationHandle> asyncOperationHandles;

    private GameObject dirtPatchGO;

    GameObject LoadPrefab(object key) {
        var handle = Addressables.LoadAssetAsync<GameObject>(key);
        asyncOperationHandles.Add(handle);
        return handle.WaitForCompletion();
    }

    void Awake() {
        asyncOperationHandles = new List<AsyncOperationHandle>();
        dirtPatchGO = LoadPrefab("Assets/Prefabs/DirtPatch.prefab");
    }

    void Start()
    {
        MakeRandomDirtPatches();
    }

    void MakeRandomDirtPatches() {
        for (int i = 0; i < 1000; i++) {
            float x = Random.Range(-100f, 100f);
            float y = Random.Range(-100f, 100f);
            GameObject dirtPatch = Instantiate<GameObject>(dirtPatchGO, Vector3.zero, Quaternion.identity);
            dirtPatch.transform.SetParent(transform);
            dirtPatch.transform.localPosition = new Vector3(x, y, 0);
        }
    }

    void OnDisable() {
        foreach (var asyncOperationHandle in asyncOperationHandles) {
            Addressables.Release(asyncOperationHandle);
        }
    }
}
