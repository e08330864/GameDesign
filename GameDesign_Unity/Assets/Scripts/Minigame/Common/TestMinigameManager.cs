using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMinigameManager : MonoBehaviour, IGameManager
{

    public GameObject minigameToSpawn;
    public Transform prefabHolder;

    Minigame game;
    MinigameInput minigameInput;

    private void Awake()
    {
        prefabHolder.gameObject.SetActive(false);

        GameObject spawned = GameObject.Instantiate(minigameToSpawn, prefabHolder.transform);
        game = spawned.GetComponent<Minigame>();
        //Neutral MinigameInput
        minigameInput = new MinigameInput(0, 0, 0, this);

        //Hard MinigameInput
        //minigameInput = new MinigameInput(1, 1, 1, this);

        //Easy MinigameInput
        //minigameInput = new MinigameInput(-1, -1, -1, this);

        game.Input = minigameInput;
    }

    private void Start()
    {
        prefabHolder.gameObject.SetActive(true);
        //Spawn Game on this Canvas, its inactive by default to allow setting MinigameInput
        game.gameObject.SetActive(true);

        //Positions of relevant Gameobjects for each Answer
        GameObject[] answerA = GameObject.FindGameObjectsWithTag("AnswerA");
        GameObject[] answerB = GameObject.FindGameObjectsWithTag("AnswerB");
        Invoke("go", 3f);
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
