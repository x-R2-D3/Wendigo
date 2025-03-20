using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int totalSheep;
    private int sheepInGoal = 0;

    [Header("Portal Settings")]
    // Instead of a portal prefab, assign the already placed portal GameObject.
    // Make sure it is inactive at the start of the game.
    public GameObject portalObject;

    private Transform portalSpawnPoint; // Used to reposition the portal, if needed.

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Optionally find the PortalSpawnPoint object automatically.
        GameObject spawnPointObject = GameObject.Find("PortalSpawnPoint");
        if (spawnPointObject != null)
        {
            portalSpawnPoint = spawnPointObject.transform;

            // Optionally, position the portal at the spawn point.
            if (portalObject != null)
            {
                portalObject.transform.position = portalSpawnPoint.position;
            }
        }
        else
        {
            Debug.LogError("PortalSpawnPoint not found! Make sure there is a GameObject named 'PortalSpawnPoint' in the scene.");
        }

        // Count all sheep in the scene.
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
        Debug.Log("All sheep are in the goal! Activating portal...");

        if (portalObject != null)
        {
            // Instead of instantiating a new portal, activate the existing one.
            portalObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Portal object is not assigned! Please assign it in the Inspector.");
        }
    }
}

