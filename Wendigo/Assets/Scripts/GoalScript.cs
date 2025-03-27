using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Sensor")) // Or change this tag if needed
        {
            // --- Check for RENAMED HapticsGameManager instance ---
            if (HapticsGameManager.Instance != null)
            {
                // --- Call RENAMED method on HapticsGameManager ---
                HapticsGameManager.Instance.RegisterItemInGoal(other.gameObject, this.gameObject);
            }
            else
            {
                // --- UPDATED ERROR MESSAGE ---
                Debug.LogError($"{nameof(HapticsGameManager)} Instance not found!", this);
            }
        }
    }
}