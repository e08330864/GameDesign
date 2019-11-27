using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatchingAnswerGame : MinigameController
{

    public GameObject aAnswerObject;
    public GameObject bAnswerObject;

    public Animator aAnimation;
    public Animator bAnimation;

    private Answer aAnswer;
    private Answer bAnswer;

    private float timeLimit = 10.0f;
    private float timeLeft;

    private float speed = 0.5f;
    private float aSpeed;
    private float bSpeed;
    private int aDifficulty;
    private int bDifficulty;

    private Stress stress;

    private new void Awake()
    {
        base.Awake();
        stress = FindObjectOfType<Stress>();
        aAnswer = Answer.A;
        aAnswerObject.GetComponentInChildren<Text>().text = "JA";

        bAnswer = Answer.B;
        bAnswerObject.GetComponentInChildren<Text>().text = "NEIN";

        aAnimation.speed = speed + 0.1f * aDifficulty;
        bAnimation.speed = speed + 0.1f * bDifficulty;
        StartCoroutine(AnswerOnAnimationFinish());
    }

    private IEnumerator AnswerOnAnimationFinish()
    {
        yield return new WaitUntil(() => aAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        stress.SetValue(stress.Value - 2);
        FinishLevel(Answer.None, silentTimelineText);
    }

    public override void StartLevel()
    {
        bAnimation.enabled = true;
        aAnimation.enabled = true;
    }

    public void AnswerClicked()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        aAnswerObject.GetComponent<Button>().interactable = false;
        bAnswerObject.GetComponent<Button>().interactable = false;

        if (clicked == aAnswerObject)
        {
            aAnimation.enabled = false;
            stress.SetValue(stress.Value - 1);
            FinishLevel(aAnswer, yesTimelineText);
        }
        else if(clicked == bAnswerObject)
        {
            bAnimation.enabled = false;
            //bstress.SetValue(stress.Value - 1);
            FinishLevel(bAnswer, noTimelineText);
        }
    }
}
