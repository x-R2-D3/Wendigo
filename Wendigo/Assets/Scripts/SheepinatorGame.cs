using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SheepinatorGame : MonoBehaviour
{
    public VRButtonTouch[] colorButtons; // Array of 4 buttons
    public float sequenceDelay = 1f;
    public float buttonLightDuration = 0.5f;

    private List<int> sequence = new List<int>();
    private int playerIndex = 0;
    private bool isPlayingSequence = false;
    private bool isPlayerTurn = false;
    private int currentRound = 0;
    private const int MAX_ROUNDS = 4;

    public void BeginGame()
    {
        if (isPlayingSequence || isPlayerTurn) return;

        sequence.Clear();
        currentRound = 0;
        StartNewRound();
    }

    private void StartNewRound()
    {
        currentRound++;
        playerIndex = 0;
        // Add new random color to sequence
        sequence.Add(Random.Range(0, 4));
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        isPlayingSequence = true;
        yield return new WaitForSeconds(1f);

        foreach (int buttonIndex in sequence)
        {
            yield return new WaitForSeconds(sequenceDelay);
            yield return StartCoroutine(LightUpButton(buttonIndex));
        }

        isPlayingSequence = false;
        isPlayerTurn = true;
    }

    private IEnumerator LightUpButton(int buttonIndex)
    {
        // Implement visual feedback for button lighting up
        // You'll need to add this functionality to VRButtonTouch
        colorButtons[buttonIndex].LightUp();
        yield return new WaitForSeconds(buttonLightDuration);
        colorButtons[buttonIndex].LightOff();
    }

    public void OnColorButtonPressed(int buttonIndex)
    {
        if (!isPlayerTurn) return;

        if (buttonIndex == sequence[playerIndex])
        {
            playerIndex++;
            if (playerIndex >= sequence.Count)
            {
                isPlayerTurn = false;
                if (currentRound < MAX_ROUNDS)
                {
                    StartNewRound();
                }
                else
                {
                    // Game Won!
                    Debug.Log("Congratulations! You won!");
                }
            }
        }
        else
        {
            // Game Over
            isPlayerTurn = false;
            Debug.Log("Game Over! Wrong sequence!");
        }
    }
}
