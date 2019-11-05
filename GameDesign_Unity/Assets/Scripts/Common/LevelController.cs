using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelController : MonoBehaviour
{
    [TextArea]
    public string question;

    [TextArea]
    public string yesAnswer;

    [TextArea]
    public string noAnswer;

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

    protected void FinishLevel(Answer? answer = null, string timelineText = "")
    {
        FindObjectOfType<Storyboard>().FinishLevel(answer, timelineText);
    }

    public virtual void StartLevel() { }
}
