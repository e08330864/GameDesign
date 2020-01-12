using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardYesOnly : WhiteboardGame
{


    private new void Awake()
    {
        base.Awake();
        
        yesSpawner.difficulty = this.maxDifficulty;

        timeLeft = timeLimit;
        yesSpawner.Init();
    }


    public override void StartLevel()
    {
        yesSpawner.enabled = true;
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

            timelineRect.sizeDelta = new Vector2(1920 * timeLeft/timeLimit, timelineRect.sizeDelta.y);
            if (timeLeft <= 0)
            {
                Storyboard story = FindObjectOfType<Storyboard>();
                story.GameOver("Dein Boss hat dich gefeuert, ohne disen Job wird sich dein Leben gewaltig verändern...");
            }
            yield return null;
        }
    }

    public new void Answered(PostItSpawner finishedSpawner)
    {
        yesSpawner.enabled = false;
        this.enabled = false;

        if (yesSpawner == finishedSpawner)
        {
            var yes = new Answer(AnswerValue.YES, yesTimelineText, yesDeltas);
            FinishLevel(yes);
        }
    }
}
