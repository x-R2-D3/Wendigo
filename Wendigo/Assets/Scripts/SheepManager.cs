using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
    public static SheepManager Instance;

    private int totalSheep;

    private HashSet<GameObject> sheepInGoal = new HashSet<GameObject>();

    [Header("Settings")]

    public AudioSource winAudioSource;
    public GameObject portal;

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
        totalSheep = GameObject.FindGameObjectsWithTag("Sheep").Length;
       
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sheep"))
        {

            if (!sheepInGoal.Contains(other.gameObject))
            {
                sheepInGoal.Add(other.gameObject);
                CheckWinCondition();
            }
        }
    }

    void CheckWinCondition()
    {
        if (!hasWon && sheepInGoal.Count >= 4)
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


        foreach (var Sheep in sheepInGoal)
        {
            Destroy(Sheep);
        }
    }


    void WinGame()
    {
        
        if (winAudioSource != null)
        {
            winAudioSource.Play();
            portal.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Win audio source is missing!");
            Debug.LogWarning("Missing portal assignment!");
        }
    }
}
