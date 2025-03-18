using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class VRButtonDepress : MonoBehaviour, IPointerClickHandler
{
    // Set this in the Inspector to correspond with the SimonSaysGame button index.
    public int buttonIndex;
    // Assign the GameObject that has the SimonSaysGame component.
    public SimonSaysGame gameManager;
    // How far the button should move when pressed.
    public Vector3 depressedOffset = new Vector3(0, -0.1f, 0);
    // Duration of the depression animation.
    public float depressDuration = 0.1f;

    // Called when the VR pointer clicks this object.
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Depress());
    }

    private IEnumerator Depress()
    {
        Vector3 originalPos = transform.position;
        Vector3 depressedPos = originalPos + depressedOffset;
        float elapsed = 0f;

        // Animate button moving down.
        while (elapsed < depressDuration)
        {
            transform.position = Vector3.Lerp(originalPos, depressedPos, elapsed / depressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = depressedPos;

        // Wait a moment before returning.
        yield return new WaitForSeconds(0.2f);

        // Animate button moving back to original position.
        elapsed = 0f;
        while (elapsed < depressDuration)
        {
            transform.position = Vector3.Lerp(depressedPos, originalPos, elapsed / depressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;

        // Now that the depressed animation is complete, notify the game manager.
        gameManager.OnColorButtonPressed(buttonIndex);
    }
}
