using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{

    void Start()
    {
        MakeRandomDirtPatches();
        MakeRandomTrees();
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

    void MakeRandomTrees() {
        GameManager gameManager = FindObjectOfType<GameManager>();
        for (int i = 0; i < 100; i++) {
            Vector2 pos = gameManager.GetRandomLocation() * 1.5f;
            GameObject[] treePrefabs = PrefabsManager.getInstance().treePrefabs;
            GameObject treePrefab = treePrefabs[(int)Random.Range(0, treePrefabs.Length)];
            GameObject tree = Instantiate<GameObject>(treePrefab, Vector3.zero, Quaternion.identity);
            tree.transform.SetParent(transform);
            tree.transform.localPosition = new Vector3(pos.x, pos.y, 0);
        }

        for (int i = 0; i < 100; i++) {
            Vector2 pos = gameManager.GetRandomLocation() * 1.5f;
            GameObject fishTree = Instantiate<GameObject>(PrefabsManager.getInstance().fishTreePrefab, Vector3.zero, Quaternion.identity);
            fishTree.transform.SetParent(transform);
            fishTree.transform.localPosition = new Vector3(pos.x, pos.y, 0);
        }
    }

}
