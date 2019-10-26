using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMinigameManager : MonoBehaviour, IGameManager
{

    public GameObject minigameToSpawn;

    Minigame game;
    private void Start()
    {
        MinigameInput minigameInput;
        //Neutral MinigameInput
        minigameInput = new MinigameInput(0, 0, 0, this);
        
        //Hard MinigameInput
        //minigameInput = new MinigameInput(1, 1, 1, this);
        
        //Easy MinigameInput
        //minigameInput = new MinigameInput(-1, -1, -1, this);

        //Spawn Game on this Canvas, its inactive by default to allow setting MinigameInput
        GameObject spawned = GameObject.Instantiate(minigameToSpawn, this.transform);
        game = spawned.GetComponent<Minigame>();
        game.Input = minigameInput;
        spawned.SetActive(true);

        //Positions of relevant Gameobjects for each Answer
        GameObject[] answerA = GameObject.FindGameObjectsWithTag("AnswerA");
        GameObject[] answerB = GameObject.FindGameObjectsWithTag("AnswerB");
        Invoke("go", 1.0f);
    }

    public void FinishGame(Answer answer)
    {
        Debug.Log("Game Manager: Game finished with Answer: " + answer);
    }

    public void go()
    {
        game.BeginGame();
    }
}
