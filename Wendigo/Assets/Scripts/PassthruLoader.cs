using UnityEngine;
using UnityEngine.SceneManagement;

public class PassthruLoader : MonoBehaviour
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
            SceneManager.LoadSceneAsync(6);
            SceneManager.UnloadSceneAsync(5);
        }
    }
}