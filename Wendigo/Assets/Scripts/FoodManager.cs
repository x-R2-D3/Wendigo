using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;

    private int totalFood;
 
    private HashSet<GameObject> foodInGoal = new HashSet<GameObject>();

    [Header("Settings")]

    public AudioSource winAudioSource;


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

 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
           
            if (!foodInGoal.Contains(other.gameObject))
            {
                foodInGoal.Add(other.gameObject);
                Debug.Log("Food entered goal: " + foodInGoal.Count + " / 4");
                CheckWinCondition();
            }
        }
    }

    void CheckWinCondition()
    {
        if (!hasWon && foodInGoal.Count >= 4)
        {
            hasWon = true;
            StartCoroutine(WinRoutine());
        }
    }


    IEnumerator WinRoutine()
    {
        WinGame();

        if (winAudioSource != null && winAudioSource.clip != null)
        {
            yield return new WaitForSeconds(winAudioSource.clip.length);
        }


        foreach (var food in foodInGoal)
        {
            Destroy(food);
        }
    }


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
