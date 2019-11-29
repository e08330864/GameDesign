using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelController : MonoBehaviour
{

    protected void FinishLevel(AnswerValue answerValue = AnswerValue.None, string timelineText = "", Character character = null, string gameShortText = "")
    {
        FindObjectOfType<Storyboard>().FinishLevel(answerValue, timelineText, character, gameShortText);
    }

    public virtual void StartLevel() { }
}
