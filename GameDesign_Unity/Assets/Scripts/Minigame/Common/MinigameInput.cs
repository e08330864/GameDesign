using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInput
{
    public float TimeScale { get; private set; }
    public float ADifficulty { get; private set; }
    public float BDifficulty { get; private set; }
    public IGameManager GameManager { get; private set; }

    /// <summary>
    /// Holds all input information for a Minigame.
    /// </summary>
    /// <param name="timeScale">Affects the duration a player has to complete the game, range [-1,1].</param
    /// <param name="aDifficulty">Affects the difficulty to 'catch' the first answer, range [-1,1].</param>
    /// <param name="bDifficulty">Affects the difficulty to 'catch' the second answer, range [-1,1].</param>
    /// <param name="gameManager">Reference to the GameManager which spawned the minigame.</param>
    public MinigameInput(float timeScale, float aDifficulty, float bDifficulty, IGameManager gameManager)
    {
        TimeScale = timeScale;
        ADifficulty = aDifficulty;
        BDifficulty = bDifficulty;
        GameManager = gameManager;
    }
}
