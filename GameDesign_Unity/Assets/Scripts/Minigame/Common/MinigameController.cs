using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : LevelController
{

    [TextArea]
    public string question;

    [TextArea]
    public string yesAnswer;

    [TextArea]
    public string yesTimelineText;

    [TextArea]
    public string noAnswer;

    [TextArea]
    public string noTimelineText;

    [TextArea]
    public string silentTimelineText;

    public string personName;

    public Sprite personImage;

    public void Awake()
    {
        Panel panel = FindObjectOfType<Panel>();
        panel.question.text = question;
        panel.yesAnswer.text = yesAnswer;
        panel.noAnswer.text = noAnswer;
        panel.personName.text = personName;
        panel.personImage.sprite = personImage;
    }

    internal void FinishLevel(Answer answer, string timelineText)
    {
        FindObjectOfType<Storyboard>().FinishLevel(answer, timelineText);
    }
}
