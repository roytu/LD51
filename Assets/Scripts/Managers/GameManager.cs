using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    private List<AsyncOperationHandle> asyncOperationHandles;

    private float gameTime = 0;

    private float waveTime = 0;
    private int wave = 0;

    private int maxWeeds = 200;

    [SerializeField]
    private TMPro.TMP_Text uiTimeText;
    [SerializeField]
    private TMPro.TMP_Text uiWeedCountText;

    [SerializeField]
    private GameObject loseCanvas;

    private bool isPlaying = true;

    void Start()
    {
        // Give the player a starting whacker
        GameObject defaultWhackerGO = Instantiate<GameObject>(PrefabsManager.getInstance().topoWhackerPrefab, Vector3.zero, Quaternion.identity);
        Whacker defaultWhacker = defaultWhackerGO.GetComponent<Whacker>();

        gameTime = 0;
        waveTime = 10f;
        wave = 0;
        isPlaying = true;

        Player player = FindObjectOfType<Player>();
        player.SetWhacker(defaultWhacker);

        SpawnWave();
    }

    void Update()
    {
        if (isPlaying) {
            HandleWave();
            gameTime += Time.deltaTime;
            uiTimeText.SetText($"Time: {Mathf.FloorToInt(gameTime)} s");

            // Count weeds
            GameObject[] weeds = GameObject.FindGameObjectsWithTag("Weed");
            if (weeds.Length > maxWeeds) {
                Lose();
            }
            uiWeedCountText.SetText($"Weeds: {weeds.Length} / {maxWeeds}");
        }
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
            Instantiate<GameObject>(PrefabsManager.getInstance().normalWeedPrefab, position, Quaternion.identity);
        }

    }

    public Vector3 GetRandomEnemySpawn() {
        // Find a location for an enemy to spawn
        float x = UnityEngine.Random.Range(-25f, 25f);
        float y = UnityEngine.Random.Range(-25f, 25f);
        return new Vector3(x, y, 0);
    }

    public Vector3 GetRandomLocation() {
        float x = UnityEngine.Random.Range(-20f, 20f);
        float y = UnityEngine.Random.Range(-20f, 20f);
        return new Vector3(x, y, 0);
    }

    void Lose() {
        loseCanvas.SetActive(true);
        isPlaying = false;
    }

    void Restart() {
        SceneManager.LoadScene("Game");
    }

    public void OnPlayAgainButtonPressed() {
        Restart();
    }

    public void OnMainMenuButtonPressed() {
        SceneManager.LoadScene("MainMenu");
    }

}
