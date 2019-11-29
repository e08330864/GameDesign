using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : LevelController
{
    [TextArea]
    public string gameShortText;

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

    public Character character;

    public void Awake()
    {
        Panel panel = FindObjectOfType<Panel>();
        panel.question.text = question;
        panel.yesAnswer.text = yesAnswer;
        panel.noAnswer.text = noAnswer;
        panel.personName.text = character.characterName;
        panel.personImage.sprite = character.figureImage;
    }

    internal void FinishLevel(AnswerValue answerValue, string timelineText)
    {
        FindObjectOfType<Storyboard>().FinishLevel(answerValue, timelineText, character, gameShortText);
    }
}
