using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;
    private int totalFood;
    private int foodInGoal = 0;

    [Header("Portal Settings")]
    public GameObject portalPrefab; // Assign in Inspector
    public AudioClip winSound; // Assign in Inspector
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        totalFood = GameObject.FindGameObjectsWithTag("Interactable").Length;
    }

    public void FoodEnteredGoal()
    {
        foodInGoal++;
        Debug.Log("Food Blocks in goal: " + foodInGoal + "/" + totalFood);

        if (foodInGoal >= totalFood)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("Yay");
        if (winSound != null && audioSource != null)
        {
            audioSource.clip = winSound;
            audioSource.Play();
        }

        //if (portalSpawnPoint != null)
        //{
        //    Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
        //}
        //else
        //{
        //    Debug.LogError("Portal spawn point is missing! Make sure 'PortalSpawnPoint' exists in the scene.");
        //}
    }
}

