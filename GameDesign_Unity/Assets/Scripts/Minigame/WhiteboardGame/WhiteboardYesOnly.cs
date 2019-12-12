using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardYesOnly : MinigameController
{
    [Space(20)]
    [Header("Whiteboard Game Settings")]
    public PostItSpawner yesSpawner;
    public float initialDelay;

    private float timeLimit = 15.0f;
    private float timeLeft;


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
            if (timeLeft <= 0)
            {
                Storyboard story = FindObjectOfType<Storyboard>();
                story.GameOver("Dein Chef hat dich gefeuert.");
            }
            yield return null;
        }
    }

    public void Answered(PostItSpawner finishedSpawner)
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
