using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGICallback : MonoBehaviour
{
    private bool waiting = false;

    // minigame return values
    private float energy;
    private float patience;
    private MGIAnswer givenAnswer;

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// Function is called before minigame is started
    /// </summary>
    public void WaitForMinigame()
    {
        if (!waiting)
        {
            waiting = true;
            CheckMinigameResult();
        }
    }

    IEnumerator CheckMinigameResult()
    {
        yield return new WaitUntil(() => !waiting);
    }

    //--------------------------------------------------------------------------------------------------
    /// <summary>
    /// Function must be called from minigame at the end, to return the minigame result and indicate proceeding
    /// </summary>
    /// <param name="energy">The delta value for energy</param>
    /// <param name="patience">The delta value for patience</param>
    /// <param name="givenAnswer">The given answer object; NULL, if no answer was given</param>
    public void MinigameResult(float energy, float patience, MGIAnswer givenAnswer)
    {
        this.energy = energy;
        this.patience = patience;
        this.givenAnswer = givenAnswer;
        waiting = false;
    }
}
