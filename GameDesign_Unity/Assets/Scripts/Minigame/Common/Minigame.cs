﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public MinigameInput Input;

    public abstract void BeginGame();

    protected void Finish(Answer answer)
    {
        Debug.Log("Answer: " + answer);
        if(Input.GameManager == null)
        {
            Debug.LogError("No GameManager set for this Minigame.");
            return;
        }
        Input.GameManager.FinishGame(answer);
    }


}
