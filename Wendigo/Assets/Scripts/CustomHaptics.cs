using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class CustomHaptics : MonoBehaviour

{
    private bool pickedUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void held()
    {
        pickedUp = true;
        StartCoroutine(CustomVibe());
    }

    IEnumerator CustomVibe()
    {
        yield return new WaitForSeconds(0.1f);

        GetComponent<HapticImpulsePlayer>().SendHapticImpulse(0.1f, 1f, 100f);
        yield return new WaitForSeconds(2f);
        GetComponent<HapticImpulsePlayer>().SendHapticImpulse(0.1f, 3f, 100f);
        yield return new WaitForSeconds(2f);

        if (pickedUp)
        {
            StartCoroutine(CustomVibe());
        }
    }

    public void released()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
