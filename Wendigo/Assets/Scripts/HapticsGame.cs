using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- RENAMED CLASS ---
public class HapticsGameManager : MonoBehaviour
{
    // --- RENAMED STATIC INSTANCE ---
    public static HapticsGameManager Instance;

    [Header("Tracking")]
    [Tooltip("Assign the specific item GameObject you want to track.")]
    public GameObject targetItem; 

    [Header("Win Condition")]
    [Tooltip("Number of distinct goals the target item must enter.")]
    public int requiredGoals = 3;

    [Header("Win Effects")]
    public AudioSource winAudioSource;
    public GameObject portal; 

   
    private HashSet<GameObject> goalsVisitedByTarget = new HashSet<GameObject>();

    private bool hasWon = false;

    void Awake()
    {
        // --- UPDATED INSTANCE CHECK ---
        if (Instance == null)
        {
            Instance = this;
            // Optional: DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // --- Crucial Check for Renamed Variable ---
        if (targetItem == null)
        {
            // --- UPDATED ERROR MESSAGE ---
            Debug.LogError($"ERROR: Target Item is not assigned in the {nameof(HapticsGameManager)} Inspector!", this);
            this.enabled = false;
            return;
        }
        // --- End Check ---

        if (portal != null)
        {
            portal.SetActive(false);
        }
        else
        {
            // --- UPDATED WARNING MESSAGE ---
            Debug.LogWarning($"Portal GameObject not assigned in {nameof(HapticsGameManager)}.", this);
        }

        // --- UPDATED LOG MESSAGE ---
        Debug.Log($"{nameof(HapticsGameManager)} initialized. Tracking item: '{targetItem.name}'. Required goals: {requiredGoals}");
    }

    
    public void RegisterItemInGoal(GameObject itemObject, GameObject goalObject)
    {
        
        if (hasWon || itemObject != targetItem)
        {
            return;
        }

        // Try to add the goalObject to the set.
        bool newGoalVisited = goalsVisitedByTarget.Add(goalObject);

        // Only proceed if this is a *new* distinct goal for the target item
        if (newGoalVisited)
        {
            // --- UPDATED LOG MESSAGE ---
            Debug.Log($"Target Item '{targetItem.name}' entered new goal '{goalObject.name}'. Total distinct goals: {goalsVisitedByTarget.Count} / {requiredGoals}");
            CheckWinCondition();
        }
    }

    void CheckWinCondition()
    {
        // Check if the target item has visited enough unique goals
        if (!hasWon && goalsVisitedByTarget.Count >= requiredGoals)
        {
            hasWon = true;
            // --- UPDATED LOG MESSAGE ---
            Debug.Log($"Win condition met by '{targetItem.name}'!");
            StartCoroutine(WinRoutine());
        }
    }

    IEnumerator WinRoutine()
    {
        WinGame(); // Trigger win effects

        if (winAudioSource != null && winAudioSource.clip != null)
        {
            yield return new WaitForSeconds(winAudioSource.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
        }

        // Optional: Destroy the target item object after winning
        // if (targetItem != null)
        // {
        //     Debug.Log($"Destroying target item object: {targetItem.name}");
        //     Destroy(targetItem);
        // }
    }

    void WinGame()
    {
        
        if (portal != null)
        {
            portal.SetActive(true);
            
        }

        if (winAudioSource != null)
        {
            winAudioSource.Play();
        }
        else
        {
            // --- UPDATED WARNING MESSAGE ---
            Debug.LogWarning($"Win audio source is not assigned or missing clip in {nameof(HapticsGameManager)}.", this);
        }

      
    }
}