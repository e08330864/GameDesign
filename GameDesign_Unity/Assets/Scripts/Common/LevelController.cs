﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelController : MonoBehaviour
{
    public List<MinigameLevel> skipOnYes;
    public List<MinigameLevel> skipOnNo;

    protected void FinishLevel(Answer answer = null, Character character = null, string gameShortText = "")
    {
        FindObjectOfType<Storyboard>().FinishLevel(answer, character, gameShortText);
    }

    public virtual void StartLevel() { }
}
