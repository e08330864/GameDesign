using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardGame : MinigameController
{
    public PostItSpawner[] spawner = new PostItSpawner[2];
    public float initialDelay;

    private float timeLimit = 30.0f;
    private float timeLeft;

    private new void Awake()
    {
        base.Awake();
        Storyboard story = FindObjectOfType<Storyboard>();
        Answer holiday = (story.GetLevelByName("Urlaub") as MinigameLevel).answer;

        if(holiday == Answer.A)
        {
            //Player said Yes to holiday -> make yes here harder
            spawner[0].difficulty = 1;
            spawner[1].difficulty = -1;
        }
        else if(holiday == Answer.B)
        {
            //Player said No to holiday -> make yes here easier
            spawner[0].difficulty = -1;
            spawner[1].difficulty = 1;
        }
        else if (holiday == Answer.None)
        {
            spawner[0].difficulty = 1;
            spawner[1].difficulty = 1;
        }

        timeLimit = timeLimit - (3.0f * 1);
        timeLeft = timeLimit;
        spawner[0].Init();
        spawner[1].Init();
    }

    public override void StartLevel()
    {
        spawner[0].enabled = true;
        spawner[1].enabled = true;
        this.enabled = true;
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
            FinishLevel(Answer.A, yesTimelineText);
        }
        else if(spawner[1] == finishedSpawner)
        {
            FinishLevel(Answer.B, noTimelineText);
        }
    }
}
