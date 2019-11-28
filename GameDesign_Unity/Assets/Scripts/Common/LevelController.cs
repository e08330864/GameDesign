using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelController : MonoBehaviour
{

    protected void FinishLevel(Answer? answer = null, string timelineText = "", Character character = null)
    {
        FindObjectOfType<Storyboard>().FinishLevel(answer, timelineText, character);
    }

    public virtual void StartLevel() { }
}
