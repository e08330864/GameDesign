using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinigameLevel", menuName = "Level/Minigame", order = 1)]
public class MinigameLevel : Level
{
    internal Answer answer;
    internal string timelineText;

    private void OnEnable()
    {
        Debug.Log("Resetting MinigameLevels...");
        answer = Answer.NULL;
        timelineText = "";
    }
}
