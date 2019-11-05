using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatchingAnswerGame : LevelController
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

    private Energy energy;
    private Patience patience;

    private new void Awake()
    {
        base.Awake();
        energy = FindObjectOfType<Energy>();
        patience = FindObjectOfType<Patience>();
        if (aDifficulty > bDifficulty)
        {
            aAnswer = Answer.A;
            aAnswerObject.GetComponentInChildren<Text>().text = "JA";
            aAnswerObject.tag = "answerA";

            bAnswer = Answer.B;
            bAnswerObject.GetComponentInChildren<Text>().text = "NEIN";
            bAnswerObject.tag = "answerB";

            aAnimation.speed = speed + 0.1f * aDifficulty;
            bAnimation.speed = speed + 0.1f * bDifficulty;
        }
        else
        {
            aAnswer = Answer.B;
            aAnswerObject.GetComponentInChildren<Text>().text = "NEIN";
            aAnswerObject.tag = "AnswerB";

            bAnswer = Answer.A;
            bAnswerObject.GetComponentInChildren<Text>().text = "JA";
            bAnswerObject.tag = "AnswerA";

            aAnimation.speed = speed + 0.1f * aDifficulty;
            bAnimation.speed = speed + 0.1f * bDifficulty;
        }

        Debug.Log("HighwayGame: CarSpeed=" + bAnimation.speed + " BusSpeed=" + aAnimation.speed);
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
            patience.SetValue(patience.Value - 1);
            energy.SetValue(energy.Value + 1);
            FinishLevel(aAnswer); //TODO: TimelineText
        }
        else if(clicked == bAnswerObject)
        {
            bAnimation.enabled = false;
            energy.SetValue(energy.Value - 1);
            FinishLevel(bAnswer); //TODO: TimelineText
        }
    }
}
