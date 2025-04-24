using UnityEngine;
using UnityEngine.SceneManagement;

public class WendigoEnd : MonoBehaviour
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
            SceneManager.LoadSceneAsync(0);
            SceneManager.UnloadSceneAsync(6);
        }
    }
}