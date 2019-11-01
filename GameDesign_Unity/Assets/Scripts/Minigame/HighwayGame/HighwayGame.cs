using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighwayGame : LevelController
{

    public GameObject bus;
    public GameObject car;

    public Animator busAnimator;
    public Animator carAnimator;

    private Answer busAnswer;
    private Answer carAnswer;

    private float timeLimit = 10.0f;
    private float timeLeft;

    private float speed = 0.5f;
    private float busSpeed;
    private float carSpeed;
    private int aDifficulty;
    private int bDifficulty;

    private void OnEnable()
    {

        if (aDifficulty > bDifficulty)
        {
            busAnswer = Answer.A;
            bus.GetComponentInChildren<Text>().text = "A";
            bus.tag = "answerA";

            carAnswer = Answer.B;
            car.GetComponentInChildren<Text>().text = "B";
            car.tag = "answerB";

            busAnimator.speed = speed + 0.1f * aDifficulty;
            carAnimator.speed = speed + 0.1f * bDifficulty;
        }
        else
        {
            busAnswer = Answer.B;
            bus.GetComponentInChildren<Text>().text = "B";
            bus.tag = "AnswerB";

            carAnswer = Answer.A;
            car.GetComponentInChildren<Text>().text = "A";
            car.tag = "AnswerA";

            busAnimator.speed = speed + 0.1f * aDifficulty;
            carAnimator.speed = speed + 0.1f * bDifficulty;
        }

        Debug.Log("HighwayGame: CarSpeed=" + carAnimator.speed + " BusSpeed=" + busAnimator.speed);
    }

    public void AnswerClicked()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        bus.GetComponent<Button>().interactable = false;
        car.GetComponent<Button>().interactable = false;

        if (clicked == bus)
        {
            busAnimator.enabled = false;
            FinishLevel(busAnswer);
        }
        else if(clicked == car)
        {
            carAnimator.enabled = false;
            FinishLevel(carAnswer);
        }
    }
}
