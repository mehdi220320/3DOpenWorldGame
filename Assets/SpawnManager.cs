using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject bearPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private int heartCount = 100;
    [SerializeField] private int bearCount = 500;
    [SerializeField] private float spawnHeightOffset = 2f; // Small offset above terrain (adjust as needed)
    [SerializeField] private Terrain terrain;

    [Header("UI")]
    [SerializeField] private TMP_Text killsText;

    private List<GameObject> activeBears = new List<GameObject>();
    private int totalKills = 0;

    void Start()
    {
        if (terrain == null)
        {
            terrain = Terrain.activeTerrain;
            if (terrain == null)
            {
                Debug.LogError("No terrain found! Please assign a terrain in the inspector.");
                return;
            }
        }

        SpawnHearts();
        SpawnBears();
        UpdateKillsUI();
    }

    void Update()
    {
        // Check for dead bears and respawn them
        for (int i = activeBears.Count - 1; i >= 0; i--)
        {
            if (activeBears[i] == null)
            {
                // Bear was destroyed, increment kills and respawn
                totalKills++;
                UpdateKillsUI();
                
                // Spawn a new bear
                SpawnBear();
                
                // Remove the null reference
                activeBears.RemoveAt(i);
            }
        }
    }

    void SpawnHearts()
    {
        if (heartPrefab == null)
        {
            Debug.LogWarning("Heart prefab not assigned!");
            return;
        }

        for (int i = 0; i < heartCount; i++)
        {
            Vector3 spawnPos = GetRandomTerrainPosition();
            Instantiate(heartPrefab, spawnPos, Quaternion.identity);
        }

        Debug.Log($"Spawned {heartCount} hearts");
    }

    void SpawnBears()
    {
        if (bearPrefab == null)
        {
            Debug.LogWarning("Bear prefab not assigned!");
            return;
        }

        for (int i = 0; i < bearCount; i++)
        {
            SpawnBear();
        }

        Debug.Log($"Spawned {bearCount} bears");
    }

    void SpawnBear()
    {
        Vector3 spawnPos = GetRandomTerrainPosition();
        GameObject newBear = Instantiate(bearPrefab, spawnPos, Quaternion.identity);
        activeBears.Add(newBear);
    }

    Vector3 GetRandomTerrainPosition()
    {
        // Get terrain bounds
        Vector3 terrainSize = terrain.terrainData.size;
        Vector3 terrainPosition = terrain.transform.position;

        // Random X and Z within terrain bounds
        float randomX = Random.Range(0, terrainSize.x);
        float randomZ = Random.Range(0, terrainSize.z);

        // Calculate world position
        float worldX = randomX + terrainPosition.x;
        float worldZ = randomZ + terrainPosition.z;

        // Get the exact height at this position on the terrain
        float terrainHeight = terrain.SampleHeight(new Vector3(worldX, 0, worldZ));

        // Spawn directly on terrain surface with small offset
        Vector3 spawnPosition = new Vector3(
            worldX,
            terrainHeight + terrainPosition.y + spawnHeightOffset,
            worldZ
        );

        return spawnPosition;
    }

    void UpdateKillsUI()
    {
        if (killsText != null)
        {
            killsText.text = $"Kills: {totalKills}";
        }
    }

    // Public method to manually add kills (if needed from other scripts)
    public void AddKill()
    {
        totalKills++;
        UpdateKillsUI();
    }

    // Get current kill count
    public int GetKillCount()
    {
        return totalKills;
    }
}