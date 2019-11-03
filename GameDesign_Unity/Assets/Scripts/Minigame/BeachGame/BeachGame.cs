using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeachGame : LevelController
{

    public GameObject sun;
    public GameObject crab;

    public Animator sunAnimator;
    public Animator crabAnimator;

    private Answer sunAnswer;
    private Answer crabAnswer;

    private float timeLimit = 10.0f;
    private float timeLeft;

    private float speed = 0.5f;
    private float sunSpeed;
    private float crabSpeed;
    private float aDifficulty;
    private float bDifficulty;

    private void Awake()
    {
        if (bDifficulty > aDifficulty)
        {
            sunAnswer = Answer.A;
            sun.GetComponentInChildren<Text>().text = "JA";
            sun.tag = "answerA";

            crabAnswer = Answer.B;
            crab.GetComponentInChildren<Text>().text = "NEIN";
            crab.tag = "answerB";

            sunAnimator.speed = speed + 0.05f * aDifficulty;
            crabAnimator.speed = speed + 0.05f * bDifficulty;
        }
        else
        {
            sunAnswer = Answer.B;
            sun.GetComponentInChildren<Text>().text = "NEIN";
            sun.tag = "AnswerB";

            crabAnswer = Answer.A;
            crab.GetComponentInChildren<Text>().text = "JA";
            crab.tag = "AnswerA";

            sunAnimator.speed = speed + 0.05f * bDifficulty;
            crabAnimator.speed = speed + 0.05f * aDifficulty;
        }

        Debug.Log("BeachGame: CrabSpeed=" + crabAnimator.speed + " SunSpeed=" + sunAnimator.speed);
    }

    public override void StartLevel()
    {
        sunAnimator.enabled = true;
        crabAnimator.enabled = true;
    }

    public void AnswerClicked()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        sun.GetComponent<Button>().interactable = false;
        crab.GetComponent<Button>().interactable = false;

        if (clicked == sun)
        {
            sunAnimator.enabled = false;
            FinishLevel(sunAnswer);
        }
        else if(clicked == crab)
        {
            crabAnimator.enabled = false;
            FinishLevel(crabAnswer);
        }
    }
}
