using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardGame : Minigame
{
    public PostItSpawner[] spawner = new PostItSpawner[2];

    private float timeLimit = 10.0f;
    private float timeLeft;

    private void Start()
    {
        if(Input == null)
        {
            Debug.LogError("MinigameInput not set for WhiteboardGame! Starting with default values");
            Input = new MinigameInput(0, 0, 0, null);
        }

        spawner[0].difficulty = Input.ADifficulty;
        spawner[1].difficulty = Input.BDifficulty;

        timeLimit = timeLimit - (3.0f * Input.TimeScale);
        timeLeft = timeLimit;

        spawner[0].enabled = true;
        spawner[1].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        spawner[0].updateTimeLimit(timeLeft, timeLimit);
        spawner[1].updateTimeLimit(timeLeft, timeLimit);
        if(timeLeft <= 0)
        {
            Finish(Answer.None);
            this.enabled = false;
        }
    }

    public void Answered(PostItSpawner finishedSpawner)
    {
        spawner[0].enabled = false;
        spawner[1].enabled = false;
        this.enabled = false;

        if (spawner[0] == finishedSpawner)
        {
            Finish(Answer.A);
        }
        else if(spawner[1] == finishedSpawner)
        {
            Finish(Answer.B);
        }
    }
}
