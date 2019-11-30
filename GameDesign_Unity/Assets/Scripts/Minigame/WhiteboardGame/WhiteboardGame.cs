using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardGame : MinigameController
{
    public PostItSpawner[] spawner = new PostItSpawner[2];
    public float initialDelay;

    private float timeLimit = 10.0f;
    private float timeLeft;

    private Stress stress;

    private new void Awake()
    {
        base.Awake();
        stress = FindObjectOfType<Stress>();
        Storyboard story = FindObjectOfType<Storyboard>();
        Answer holiday = (story.GetLevelByName("Urlaub") as MinigameLevel).answer;

        if(holiday.answer == AnswerValue.YES)
        {
            //Player said Yes to holiday -> make yes here harder
            spawner[0].difficulty = 1;
            spawner[1].difficulty = -1;
        }
        else if(holiday.answer == AnswerValue.NO)
        {
            //Player said No to holiday -> make yes here easier
            spawner[0].difficulty = -1;
            spawner[1].difficulty = 1;
        }
        else if (holiday.answer == AnswerValue.None)
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
                var noneAnswer = new Answer(AnswerValue.None, silentTimelineText, silentDeltas);
                FinishLevel(noneAnswer);
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
            var yes = new Answer(AnswerValue.YES, yesTimelineText, yesDeltas);
            FinishLevel(yes);
        }
        else if(spawner[1] == finishedSpawner)
        {
            var no = new Answer(AnswerValue.NO, noTimelineText, noDeltas);
            FinishLevel(no);
        }
    }
}
