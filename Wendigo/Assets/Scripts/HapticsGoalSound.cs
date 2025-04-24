using UnityEngine;

public class HapticsGoalSound : MonoBehaviour


{
    public AudioSource winAudioSource;

    void Start()
    {
       
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scanner"))
        {
            winAudioSource.Play();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
