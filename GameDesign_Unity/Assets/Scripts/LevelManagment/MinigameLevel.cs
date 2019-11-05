using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinigameLevel", menuName = "Level/Minigame", order = 1)]
public class MinigameLevel : Level
{
    [HideInInspector]
    public Answer answer;
    [HideInInspector]
    public string timelineText;
}
