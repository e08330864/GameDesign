using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelController : MonoBehaviour
{

    protected void FinishLevel(Answer? answer = null, string timelineText = "")
    {
        FindObjectOfType<Storyboard>().FinishLevel(answer, timelineText);
    }

    public virtual void StartLevel() { }
}
