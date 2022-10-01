using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    private List<AsyncOperationHandle> asyncOperationHandles;

    private GameObject normalWhackerPrefab;

    void Start()
    {
        // Give the player a starting whacker
        GameObject normalWhackerGO = Instantiate<GameObject>(normalWhackerPrefab, Vector3.zero, Quaternion.identity);
        NormalWhacker normalWhacker = normalWhackerGO.GetComponent<NormalWhacker>();

        Player player = FindObjectOfType<Player>();
        player.SetWhacker(normalWhacker);
    }

    void Update()
    {
        
    }


    // Addressables stuff
    GameObject LoadPrefab(object key) {
        var handle = Addressables.LoadAssetAsync<GameObject>(key);
        asyncOperationHandles.Add(handle);
        return handle.WaitForCompletion();
    }

    void Awake() {
        asyncOperationHandles = new List<AsyncOperationHandle>();
        normalWhackerPrefab = LoadPrefab("Assets/Prefabs/Whackers/NormalWhacker.prefab");
    }

    void OnDisable() {
        foreach (var asyncOperationHandle in asyncOperationHandles) {
            Addressables.Release(asyncOperationHandle);
        }
    }

}
