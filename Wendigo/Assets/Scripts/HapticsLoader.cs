using UnityEngine;
using UnityEngine.SceneManagement;

public class HapticsLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadSceneAsync(3);
            SceneManager.UnloadSceneAsync(2);
        }
    }
}
