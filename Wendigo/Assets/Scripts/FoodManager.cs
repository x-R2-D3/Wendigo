using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;

    // We still keep the total count for debugging, but it won't trigger the win condition.
    private int totalFood;
    // HashSet to track food objects currently inside the goal area
    private HashSet<GameObject> foodInGoal = new HashSet<GameObject>();

    [Header("Settings")]
    // Assign an AudioSource in the Inspector that has your win sound clip attached
    public AudioSource winAudioSource;

    // Flag to ensure the win condition is triggered only once
    private bool hasWon = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        totalFood = GameObject.FindGameObjectsWithTag("Food").Length;
        Debug.Log("Total Food Objects: " + totalFood);
    }

    // Called when a collider enters the goal's trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            // Add the food object only if it's not already in our set
            if (!foodInGoal.Contains(other.gameObject))
            {
                foodInGoal.Add(other.gameObject);
                Debug.Log("Food entered goal: " + foodInGoal.Count + " / 4");
                CheckWinCondition();
            }
        }
    }

    // Called when a collider exits the goal's trigger collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (foodInGoal.Contains(other.gameObject))
            {
                foodInGoal.Remove(other.gameObject);
                Debug.Log("Food exited goal: " + foodInGoal.Count + " / 4");
            }
        }
    }

    // Check if 4 food objects are inside the goal simultaneously
    void CheckWinCondition()
    {
        if (!hasWon && foodInGoal.Count >= 4)
        {
            hasWon = true;
            StartCoroutine(WinRoutine());
        }
    }

    // Coroutine that plays the win sound, waits for it to finish, then destroys all food objects inside the goal
    IEnumerator WinRoutine()
    {
        WinGame();

        if (winAudioSource != null && winAudioSource.clip != null)
        {
            yield return new WaitForSeconds(winAudioSource.clip.length);
        }

        // Destroy all food objects that are inside the goal
        foreach (var food in foodInGoal)
        {
            Destroy(food);
        }
    }

    // Plays the win sound
    void WinGame()
    {
        Debug.Log("You Win!");
        if (winAudioSource != null)
        {
            winAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Win audio source is missing!");
        }
    }
}
