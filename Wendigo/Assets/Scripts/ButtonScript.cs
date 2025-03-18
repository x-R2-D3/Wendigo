using UnityEngine;
using System.Collections;

public class VRButtonTouch : MonoBehaviour
{
    public int buttonIndex;
    public SheepinatorGame gameManager;
    public Color buttonColor; // Set this in inspector for each button

    private MeshRenderer meshRenderer;
    private Material buttonMaterial;
    public Vector3 depressedOffset = new Vector3(0, -0.1f, 0);
    public float depressDuration = 0.1f;
    private bool isPressed = false;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        buttonMaterial = meshRenderer.material;
    }

    public void LightUp()
    {
        buttonMaterial.EnableKeyword("_EMISSION");
        buttonMaterial.SetColor("_EmissionColor", buttonColor * 2f);
    }

    public void LightOff()
    {
        buttonMaterial.DisableKeyword("_EMISSION");
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
