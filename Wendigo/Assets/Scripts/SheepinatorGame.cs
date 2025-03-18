using UnityEngine;
using System.Collections;

public class VRButtonTouch : MonoBehaviour
{
    public int buttonIndex;
    public SheepinatorGame gameManager;

    // For visual feedback (using the object's MeshRenderer instead of a UI Image)
    private MeshRenderer meshRenderer;

    public Vector3 depressedOffset = new Vector3(0, -0.1f, 0);
    public float depressDuration = 0.1f;
    private bool isPressed = false;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
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
        while (elapsed < depressDuration)
        {
            transform.position = Vector3.Lerp(originalPos, depressedPos, elapsed / depressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = depressedPos;

        yield return new WaitForSeconds(0.2f);

        elapsed = 0f;
        while (elapsed < depressDuration)
        {
            transform.position = Vector3.Lerp(depressedPos, originalPos, elapsed / depressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;

        gameManager.OnColorButtonPressed(buttonIndex);
        isPressed = false;
    }
}
