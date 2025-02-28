using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Scrambler : MonoBehaviour
{
    public enum ScrambleEffect { AntiGravity }
    public ScrambleEffect effectType;

    private void Start()
    {
        // Ensure the effect type is set to AntiGravity
        effectType = ScrambleEffect.AntiGravity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            XROriginEffects playerEffects = other.GetComponent<XROriginEffects>();
            if (playerEffects != null)
            {
                if (effectType == ScrambleEffect.AntiGravity)
                {
                    playerEffects.EnableAntiGravity();
                }
            }

            // Destroy the scrambler after it's picked up
            Destroy(gameObject);
        }
    }
}
