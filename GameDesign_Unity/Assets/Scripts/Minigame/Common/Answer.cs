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
    IGNORE
}

public class Answer
{
    public AnswerValue answer = AnswerValue.NULL;
    public string timeLineText = "";
    public ResourceDeltas deltas;

    public Answer() { }

    public Answer(AnswerValue answer, string timeLineText, ResourceDeltas deltas)
    {
        this.answer = answer;
        this.timeLineText = timeLineText;
        this.deltas = deltas;
    }
}