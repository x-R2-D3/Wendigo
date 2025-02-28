using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int totalSheep;
    private int sheepInGoal = 0;

    [Header("Portal Settings")]
    public GameObject portalPrefab; // Assign in Inspector
    private Transform portalSpawnPoint; // Now assigned automatically in Start()

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Find the PortalSpawnPoint object automatically
        GameObject spawnPointObject = GameObject.Find("PortalSpawnPoint");

        if (spawnPointObject != null)
        {
            portalSpawnPoint = spawnPointObject.transform;
        }
        else
        {
            Debug.LogError("PortalSpawnPoint not found! Make sure there is a GameObject named 'PortalSpawnPoint' in the scene.");
        }

        // Count all sheep in the scene
        totalSheep = GameObject.FindGameObjectsWithTag("Sheep").Length;
    }

    public void SheepEnteredGoal()
    {
        sheepInGoal++;
        Debug.Log("Sheep in goal: " + sheepInGoal + "/" + totalSheep);

        if (sheepInGoal >= totalSheep)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("All sheep are in the goal! Spawning portal...");

        if (portalSpawnPoint != null)
        {
            Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Portal spawn point is missing! Make sure 'PortalSpawnPoint' exists in the scene.");
        }
    }
}

