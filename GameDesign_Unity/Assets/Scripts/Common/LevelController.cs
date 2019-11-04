using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelController : MonoBehaviour
{
    protected void FinishLevel(Answer? answer = null)
    {
        FindObjectOfType<Storyboard>().FinishLevel(answer);
    }

    public virtual void StartLevel() { }
}
