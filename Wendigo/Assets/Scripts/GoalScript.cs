using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Sensor")) // Or change this tag if needed
        {
            
            if (HapticsGameManager.Instance != null)
            {
                
                HapticsGameManager.Instance.RegisterItemInGoal(other.gameObject, this.gameObject);
            }
            else
            {
                
                Debug.LogError($"{nameof(HapticsGameManager)} Instance not found!", this);
            }
        }
    }
}