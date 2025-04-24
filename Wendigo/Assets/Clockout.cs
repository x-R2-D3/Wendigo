using UnityEngine;
using UnityEngine.SceneManagement;

public class Clockout : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        // Load the game scene (index 1) and unload the current scene (index 0)



        SceneManager.LoadSceneAsync(5);
        SceneManager.UnloadSceneAsync(0);

    }
}