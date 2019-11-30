using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : LevelController
{
    [TextArea]
    public string gameShortText;

    [TextArea]
    public string question;

    [Space(20)]

    [TextArea]
    public string yesAnswer;
    [TextArea]
    public string yesTimelineText;
    public ResourceDeltas yesDeltas;

    [Space(20)]

    [TextArea]
    public string noAnswer;
    [TextArea]
    public string noTimelineText;
    public ResourceDeltas noDeltas;

    [Header("Silent Answer")]
    [TextArea]
    public string silentTimelineText;
    public ResourceDeltas silentDeltas;

    [Space(20)]

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

    internal void FinishLevel(Answer answer)
    {
        FindObjectOfType<Storyboard>().FinishLevel(answer, character, gameShortText);
    }
}
