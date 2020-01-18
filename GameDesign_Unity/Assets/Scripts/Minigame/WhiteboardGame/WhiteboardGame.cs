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
    public string yesButtonOverride;
    public string noButtonOverride;
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
                var noneAnswer = new Answer(AnswerValue.IGNORE, silentTimelineText, silentDeltas);
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
            if (yesButtonOverride != "") yesButton.GetComponentInChildren<Text>().text = yesButtonOverride;
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
            if (noButtonOverride != "") noButton.GetComponentInChildren<Text>().text = noButtonOverride;
            b.onClick.AddListener( () =>
            {
                var no = new Answer(AnswerValue.NO, noTimelineText, noDeltas);
                FinishLevel(no);
                Destroy(noButton);
            });
        }
    }
}
