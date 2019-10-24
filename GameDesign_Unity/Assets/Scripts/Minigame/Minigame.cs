using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public MinigameInput Input { get; set; }

    public Transform[] APositions;

    public Transform[] BPositions;

    protected void Finish()
    {
        //Input.GameManager.FinishGame(this);
    }
}
