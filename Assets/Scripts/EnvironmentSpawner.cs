using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{

    void Start()
    {
        MakeRandomDirtPatches();
    }

    void MakeRandomDirtPatches() {
        for (int i = 0; i < 1000; i++) {
            float x = Random.Range(-100f, 100f);
            float y = Random.Range(-100f, 100f);
            GameObject dirtPatch = Instantiate<GameObject>(PrefabsManager.getInstance().dirtPatchPrefab, Vector3.zero, Quaternion.identity);
            dirtPatch.transform.SetParent(transform);
            dirtPatch.transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
