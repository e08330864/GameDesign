using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteboardGame : MinigameController
{
    [Space(20)]
    [Header("Whiteboard Game Settings")]
    public PostItSpawner yesSpawner;
    public PostItSpawner noSpawner;
    public float initialDelay;

    public float timeMultiplier = 1;
    protected float timeLimit = 10.0f;
    protected float timeLeft;

    protected RectTransform timelineRect;

    private new void Awake()
    {
        base.Awake();
        Storyboard story = FindObjectOfType<Storyboard>();
        
        yesSpawner.difficulty = yesDifficulty;
        noSpawner.difficulty = noDifficulty;

        timeLimit = timeLimit * timeMultiplier;
        timeLeft = timeLimit;
        yesSpawner.Init();
        noSpawner.Init();
    }

    private void Start()
    {
        var timeline = Instantiate(Resources.Load("Timeline", typeof(GameObject)), this.transform) as GameObject;
        timelineRect = timeline.GetComponent<RectTransform>();
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

            timelineRect.sizeDelta = new Vector2(1920 * timeLeft/timeLimit, timelineRect.sizeDelta.y);
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
        StopAllCoroutines();

        if (yesSpawner == finishedSpawner)
        {
            GameObject yesButton = Instantiate(Resources.Load("YES", typeof(GameObject)), FindObjectOfType<Canvas>().transform) as GameObject;
            Button b = yesButton.GetComponentInChildren<Button>();
            b.onClick.AddListener( () =>
            {
                var yes = new Answer(AnswerValue.YES, yesTimelineText, yesDeltas);
                FinishLevel(yes);
                Destroy(yesButton);
            });

            
        }
        else if(noSpawner == finishedSpawner)
        {
            GameObject noButton = Instantiate(Resources.Load("NO", typeof(GameObject)), FindObjectOfType<Canvas>().transform) as GameObject;
            Button b = noButton.GetComponentInChildren<Button>();
            b.onClick.AddListener( () =>
            {
                var no = new Answer(AnswerValue.NO, noTimelineText, noDeltas);
                FinishLevel(no);
                Destroy(noButton);
            });
        }
    }
}
