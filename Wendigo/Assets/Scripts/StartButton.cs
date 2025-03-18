using UnityEngine;
using System.Collections;

public class VRStartButton : MonoBehaviour
{
    // Reference to your SimonSaysGame manager.
    public SheepinatorGame gameManager;
    
    // Controls how far the button moves when pressed.
    public Vector3 depressedOffset = new Vector3(0, -0.1f, 0);
    
    // Duration of the depress animation.
    public float depressDuration = 0.1f;
    
    // To prevent multiple triggers.
    private bool isPressed = false;
    
    // Called when another collider enters this object's trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Make sure the colliding object is your VR controller.
        if (!isPressed && other.CompareTag("VRController"))
        {
            isPressed = true;
            StartCoroutine(DepressAndStart());
        }
    }
    
    private IEnumerator DepressAndStart()
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
        
        // Optional pause for visual effect.
        yield return new WaitForSeconds(0.2f);
        
        // Animate button returning to original position.
        elapsed = 0f;
        while (elapsed < depressDuration)
        {
            transform.position = Vector3.Lerp(depressedPos, originalPos, elapsed / depressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
        
        // Start the game by calling BeginGame on the game manager.
        gameManager.BeginGame();
        
        // Allow the button to be pressed again.
        isPressed = false;
    }
}
