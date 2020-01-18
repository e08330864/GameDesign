using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatchingAnswerGame : MinigameController
{

    public GameObject yesObject;
    public GameObject noObject;

    private Animator yesAnim;
    private Animator noAnim;

    private float timeLimit = 10.0f;
    private float timeLeft;

    private float speed = 0.5f;
    private float yesSpeed;
    private float noSpeed;
    private int yesDifficulty;
    private int noDifficulty;

    private new void Awake()
    {
        base.Awake();
        yesObject.GetComponentInChildren<Text>().text = "JA";
        noObject.GetComponentInChildren<Text>().text = "NEIN";

        yesAnim = yesObject.GetComponent<Animator>();
        noAnim = noObject.GetComponent<Animator>();

        yesAnim.speed = speed + 0.1f * yesDifficulty;
        noAnim.speed = speed + 0.1f * noDifficulty;
        StartCoroutine(AnswerOnAnimationFinish());
    }


    private IEnumerator AnswerOnAnimationFinish()
    {
        yield return new WaitUntil(() => yesAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        var noneAnswer = new Answer(AnswerValue.IGNORE, silentTimelineText, silentDeltas);
        FinishLevel(noneAnswer);
    }

    public override void StartLevel()
    {
        noAnim.enabled = true;
        yesAnim.enabled = true;
    }

    public void AnswerClicked()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        yesObject.GetComponent<Button>().interactable = false;
        noObject.GetComponent<Button>().interactable = false;
        noAnim.enabled = false;
        yesAnim.enabled = false;

        if (clicked == yesObject)
        {
            var yes = new Answer(AnswerValue.YES, yesTimelineText, yesDeltas);
            FinishLevel(yes);
        }
        else if(clicked == noObject)
        {
            var no = new Answer(AnswerValue.NO, noTimelineText, noDeltas);
            FinishLevel(no);
        }
    }
}
