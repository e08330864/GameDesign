using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardGame : Minigame
{
    public PostItSpawner[] spawner = new PostItSpawner[2];
    public float initialDelay;

    private float timeLimit = 10.0f;
    private float timeLeft;

    private void Start()
    {
        this.Input = new MinigameInput(0, 0, 0, null);
        spawner[0].difficulty = Input.ADifficulty;
        spawner[1].difficulty = Input.BDifficulty;
        spawner[0].enabled = true;
        spawner[1].enabled = true;

        timeLimit = timeLimit - (3.0f * Input.TimeScale);
        timeLeft = timeLimit;
        StartCoroutine(UpdateTimeLeft());
    }

    private IEnumerator UpdateTimeLeft()
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            timeLeft -= Time.deltaTime;

            spawner[0].updateTimeLimit(timeLeft, timeLimit);
            spawner[1].updateTimeLimit(timeLeft, timeLimit);
            if (timeLeft <= 0)
            {
                Finish(Answer.None);
                this.enabled = false;
            }
            yield return null;
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

    public override void BeginGame()
    {
        this.enabled = true;
    }
}
