using UnityEngine;

public class InactiveScript : MonoBehaviour
{
    public GameObject targetObject; // The object to be deactivated
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOff()
    {
        targetObject.SetActive(false);
    }
}
