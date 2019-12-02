using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardGame : MinigameController
{
    public PostItSpawner yesSpawner;
    public PostItSpawner noSpawner;
    public float initialDelay;
    public bool checkHoliday = true; //TODO: Make this dynamic for different Levels...

    private float timeLimit = 10.0f;
    private float timeLeft;


    private new void Awake()
    {
        base.Awake();
        Storyboard story = FindObjectOfType<Storyboard>();
        
        //TODO: Make this dynamic for different Levels...
        if (checkHoliday)
            setDifficultyHoliday(story); 
        else{
            yesSpawner.difficulty = 1;
            noSpawner.difficulty = 1;
        }

        timeLimit = timeLimit - (3.0f * 1);
        timeLeft = timeLimit;
        yesSpawner.Init();
        noSpawner.Init();
    }

    private void setDifficultyHoliday(Storyboard story)
    {
        Answer holiday = (story.GetLevelByName("Urlaub") as MinigameLevel).answer;

        if (holiday.answer == AnswerValue.YES)
        {
            //Player said Yes to holiday -> make yes here harder
            yesSpawner.difficulty = 1;
            noSpawner.difficulty = -1;
        }
        else if (holiday.answer == AnswerValue.NO)
        {
            //Player said No to holiday -> make yes here easier
            yesSpawner.difficulty = -1;
            noSpawner.difficulty = 1;
        }
        else if (holiday.answer == AnswerValue.None)
        {
            yesSpawner.difficulty = 1;
            noSpawner.difficulty = 1;
        }
    }

    public override void StartLevel()
    {
        yesSpawner.enabled = true;
        noSpawner.enabled = true;
        this.enabled = true;
        StartCoroutine(UpdateTimeLeft());
    }

    private IEnumerator UpdateTimeLeft()
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            timeLeft -= Time.deltaTime;

            yesSpawner.updateTimeLimit(timeLeft, timeLimit);
            noSpawner.updateTimeLimit(timeLeft, timeLimit);
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
        yesSpawner.enabled = false;
        noSpawner.enabled = false;
        this.enabled = false;

        if (yesSpawner == finishedSpawner)
        {
            var yes = new Answer(AnswerValue.YES, yesTimelineText, yesDeltas);
            FinishLevel(yes);
        }
        else if(noSpawner == finishedSpawner)
        {
            var no = new Answer(AnswerValue.NO, noTimelineText, noDeltas);
            FinishLevel(no);
        }
    }
}
