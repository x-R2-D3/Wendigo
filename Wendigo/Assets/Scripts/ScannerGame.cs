using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class ScannerGame : MonoBehaviour
{
    public static ScannerGame Instance;

    private int totalGoals;

    private HashSet<GameObject> ScannedGoals = new HashSet<GameObject>();

    [Header("Settings")]

    public AudioSource winAudioSource;
    public AudioSource scanAudio;
    public GameObject portal;
    public GameObject lights;

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
        totalGoals = GameObject.FindGameObjectsWithTag("Scan").Length;
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scan"))
        {
            //TriggerHaptics();

            if (!ScannedGoals.Contains(other.gameObject))
            {
                ScannedGoals.Add(other.gameObject);
                scanAudio.Play();

                CheckWinCondition();
            }
        }
    }

    //void TriggerHaptics()
    //{
    //    GetComponent<HapticImpulsePlayer>().SendHapticImpulse(1f, 3f, 800f);
    //}

    void CheckWinCondition()
    {
        if (!hasWon && ScannedGoals.Count >= 3)
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


        foreach (var Scan in ScannedGoals)
        {
            Destroy(Scan);
        }
    }


    void WinGame()
    {
       
        if (winAudioSource != null)
        {
            winAudioSource.Play();
            portal.SetActive(true);
            lights.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Win audio source is missing!");
        }
    }
}