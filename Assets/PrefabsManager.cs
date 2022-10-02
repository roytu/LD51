using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    public GameObject dirtPatchPrefab;
    public GameObject normalWhackerPrefab;
    public GameObject normalWhackerHitboxPrefab;
    public GameObject topoWhackerPrefab;
    public GameObject topoWhackerProjectilePrefab;
    public GameObject topoWhackerProjectileHitboxPrefab;
    public GameObject normalWeedPrefab;

    public static PrefabsManager getInstance() {
        return FindObjectOfType<PrefabsManager>();
    }
}
