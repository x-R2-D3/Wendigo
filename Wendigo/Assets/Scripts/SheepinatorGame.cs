using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SimonSaysGame : MonoBehaviour
{
    [Header("Game Settings")]
    // If you’re using world-space UI buttons, assign them here.
    public Button[] colorButtons;
    public int winRounds = 5;       // Number of rounds needed to win

    private List<int> sequence = new List<int>();
    private int currentRound = 0;
    private int playerInputIndex = 0;
    private bool isInputEnabled = false;

    void Start()
    {
        // Start the game after a short delay.
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(NextRound());
    }

    IEnumerator NextRound()
    {
        // Disable input while showing the sequence.
        isInputEnabled = false;
        playerInputIndex = 0;
        currentRound++;

        // Add a new random color (button index) to the sequence.
        sequence.Add(Random.Range(0, colorButtons.Length));

        yield return new WaitForSeconds(1f);
        // Show the sequence to the player.
        yield return StartCoroutine(PlaySequence());

        // Enable input after sequence playback.
        isInputEnabled = true;
    }

    IEnumerator PlaySequence()
    {
        // Loop through each color in the sequence.
        for (int i = 0; i < sequence.Count; i++)
        {
            int index = sequence[i];
            // For UI buttons, flash the button image.
            Color originalColor = colorButtons[index].image.color;

            // Flash effect: change the button's color to white.
            colorButtons[index].image.color = Color.white;
            yield return new WaitForSeconds(0.5f);

            // Revert to the original color.
            colorButtons[index].image.color = originalColor;
            yield return new WaitForSeconds(0.3f);
        }
    }

    // This method is called when a button is pressed (via the VR button script).
    public void OnColorButtonPressed(int buttonIndex)
    {
        if (!isInputEnabled)
            return;

        if (buttonIndex == sequence[playerInputIndex])
        {
            playerInputIndex++;

            if (playerInputIndex >= sequence.Count)
            {
                if (currentRound >= winRounds)
                {
                    Debug.Log("You win!");
                    isInputEnabled = false;
                    // Add additional win logic (e.g., show a win screen)
                }
                else
                {
                    StartCoroutine(NextRound());
                }
            }
        }
        else
        {
            Debug.Log("Wrong button! Game over. Resetting...");
            isInputEnabled = false;
            StartCoroutine(ResetGame());
        }
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1f);
        sequence.Clear();
        currentRound = 0;
        playerInputIndex = 0;
        Debug.Log("Game reset. Starting again...");
        yield return StartCoroutine(StartGame());
    }
}
