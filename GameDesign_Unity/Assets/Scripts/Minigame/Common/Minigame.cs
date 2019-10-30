using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public MinigameInput Input;
    public string minigameName;

    public abstract void BeginGame();

    protected void Finish(Answer answer)
    {
        Debug.Log("Answer: " + answer);
        GlobalAnswers.state[this.minigameName] = ""+answer;
        this.enabled = false;
        StopAllCoroutines();
    }


}
