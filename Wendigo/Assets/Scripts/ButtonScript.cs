using UnityEngine;
using System.Collections;

public class VRButtonTouch : MonoBehaviour
{
    // Set this index in the Inspector so it matches the corresponding button index in the SimonSaysGame.
    public int buttonIndex;

    // Reference to the GameManager script (SimonSaysGame) to call the button press method.
    public SimonSaysGame gameManager;

    // Visual feedback: how far the button moves when pressed.
    public Vector3 depressedOffset = new Vector3(0, -0.1f, 0);

    // Duration for the button to move down/up.
    public float depressDuration = 0.1f;

    // To prevent multiple triggers before the button resets.
    private bool isPressed = false;

    // This function is called when another collider enters the trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is tagged as "VRController".
        if (!isPressed && other.CompareTag("VRController"))
        {
            isPressed = true;
            StartCoroutine(DepressAndNotify());
        }
    }

    private IEnumerator DepressAndNotify()
    {
        Vector3 originalPos = transform.position;
        Vector3 depressedPos = originalPos + depressedOffset;
        float elapsed = 0f;

        // Animate the button moving down.
        while (elapsed < depressDuration)
        {
            transform.position = Vector3.Lerp(originalPos, depressedPos, elapsed / depressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = depressedPos;

        // Optional pause for visual effect.
        yield return new WaitForSeconds(0.2f);

        // Animate the button moving back to its original position.
        elapsed = 0f;
        while (elapsed < depressDuration)
        {
            transform.position = Vector3.Lerp(depressedPos, originalPos, elapsed / depressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;

        // Notify the game manager that this button has been pressed.
        gameManager.OnColorButtonPressed(buttonIndex);

        // Allow the button to be pressed again.
        isPressed = false;
    }
}
