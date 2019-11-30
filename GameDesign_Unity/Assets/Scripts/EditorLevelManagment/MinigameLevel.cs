using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinigameLevel", menuName = "Level/Minigame", order = 1)]
public class MinigameLevel : Level
{
    internal Answer answer = new Answer();
    internal Character character;

    private void OnEnable()
    {
        Debug.Log("Resetting MinigameLevels...");
        answer.answer = AnswerValue.NULL;
        answer.timeLineText = "";
        answer.deltas = null;
    }
}
