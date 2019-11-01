using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardGame : LevelController
{
    public PostItSpawner[] spawner = new PostItSpawner[2];
    public float initialDelay;

    private float timeLimit = 10.0f;
    private float timeLeft;

    private void Start()
    {
        spawner[0].difficulty = 1;
        spawner[1].difficulty = 1;
        spawner[0].enabled = true;
        spawner[1].enabled = true;

        timeLimit = timeLimit - (3.0f * 1);
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
                FinishLevel(Answer.None);
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
            FinishLevel(Answer.A);
        }
        else if(spawner[1] == finishedSpawner)
        {
            FinishLevel(Answer.B);
        }
    }
}
