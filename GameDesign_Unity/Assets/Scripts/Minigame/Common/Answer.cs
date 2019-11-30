using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Text))]
public enum AnswerValue
{
    NULL,
    YES,
    NO,
    None
}

public class Answer
{
    public AnswerValue answer = AnswerValue.NULL;
    public string timeLineText = "";
    public float deltaStress = 0f;
    public float deltaMoney = 0f;
    public float deltaCharacterSympathy = 0f;

    public Answer() { }

    public Answer(AnswerValue answer, string timeLineText, Vector3 deltas)
    {
        this.answer = answer;
        this.timeLineText = timeLineText;
        this.deltaStress = deltas.x;
        this.deltaMoney = deltas.y;
        this.deltaCharacterSympathy = deltas.z;
    }

    public Answer(AnswerValue answer, string timeLineText, float deltaStress, float deltaMoney, float deltaCharacterSympathy)
    {
        this.answer = answer;
        this.timeLineText = timeLineText;
        this.deltaStress = deltaStress;
        this.deltaMoney = deltaMoney;
        this.deltaCharacterSympathy = deltaCharacterSympathy;
    }
}