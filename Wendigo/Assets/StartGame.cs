using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Startgame()
    {
        SceneManager.LoadSceneAsync(2);
        SceneManager.UnloadSceneAsync(1);

    }
}
