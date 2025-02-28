using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class XROriginEffects : MonoBehaviour
{
    public float antiGravityHeight = 5f;
    public float antiGravityDuration = 5f;
    private bool isFloating = false;

    public void EnableAntiGravity()
    {
        if (!isFloating)
        {
            StartCoroutine(AntiGravityEffect());
        }
    }

    private IEnumerator AntiGravityEffect()
    {
        isFloating = true;
        Vector3 originalPosition = transform.position;
        Vector3 floatPosition = originalPosition + new Vector3(0, antiGravityHeight, 0);

        float elapsedTime = 0f;
        float duration = antiGravityDuration / 2; // Time for ascent and descent

        // Lift the player smoothly
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(originalPosition, floatPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Keep the player floating for a brief moment
        yield return new WaitForSeconds(antiGravityDuration / 2);

        elapsedTime = 0f;

        // Bring the player back down smoothly
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(floatPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isFloating = false;
        Debug.Log("Gravity restored.");
    }
}
