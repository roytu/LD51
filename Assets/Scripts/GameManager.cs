using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    private List<AsyncOperationHandle> asyncOperationHandles;

    private GameObject normalWhackerPrefab;

    public GameObject normalWeedPrefab;

    private float gameTime = 0;

    private float waveTime = 0;
    private int wave = 0;

    [SerializeField]
    private TMPro.TMP_Text uiTimeText;

    void Start()
    {
        // Give the player a starting whacker
        GameObject normalWhackerGO = Instantiate<GameObject>(normalWhackerPrefab, Vector3.zero, Quaternion.identity);
        NormalWhacker normalWhacker = normalWhackerGO.GetComponent<NormalWhacker>();

        gameTime = 0;
        waveTime = 10f;
        wave = 0;

        Player player = FindObjectOfType<Player>();
        player.SetWhacker(normalWhacker);
    }

    void Update()
    {
        HandleWave();
        gameTime += Time.deltaTime;
        uiTimeText.SetText($"Time: {gameTime}");
    }

    void HandleWave() {
        waveTime -= Time.deltaTime;
        if (waveTime < 0f) {
            waveTime = 10f;
            SpawnWave();
            wave += 1;
        }
    }

    void SpawnWave() {
        // Handle OnNewWave for existing weeds
        GameObject[] weeds = GameObject.FindGameObjectsWithTag("Weed");
        foreach (GameObject weed in weeds) {
            weed.GetComponent<Weed>().OnNewWave();
        }

        // TODO ramp up difficulty
        for (int i = 0; i < 10; i++) {
            Vector3 position = GetRandomEnemySpawn();
            GameObject normalWeedGO = Instantiate<GameObject>(normalWeedPrefab, position, Quaternion.identity);
        }
    }

    public Vector3 GetRandomEnemySpawn() {
        // Find a location for an enemy to spawn
        float x = Random.Range(-50f, 50f);
        float y = Random.Range(-25f, 25f);
        return new Vector3(x, y, 0);
    }

    public Vector3 GetRandomLocation() {
        // Find a location for an enemy to spawn
        float x = Random.Range(-50f, 50f);
        float y = Random.Range(-25f, 25f);
        return new Vector3(x, y, 0);
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
        normalWeedPrefab = LoadPrefab("Assets/Prefabs/Weeds/NormalWeed.prefab");
    }

    void OnDisable() {
        foreach (var asyncOperationHandle in asyncOperationHandles) {
            Addressables.Release(asyncOperationHandle);
        }
    }

}
