using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Text))]
public enum AnswerValue
{
    NULL,
    A,
    B,
    None
}

public class Answer
{
    public AnswerValue answer = AnswerValue.NULL;
    public string timeLineText = "";
    public float deltaStress = 0f;
    public float deltaMoney = 0f;
    public float deltaCharacterSympathy = 0f;
}