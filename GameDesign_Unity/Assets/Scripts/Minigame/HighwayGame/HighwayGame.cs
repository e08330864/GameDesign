using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighwayGame : Minigame
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

    private void OnEnable()
    {
        if (Input == null)
        {
            Debug.LogError("MinigameInput not set for HighwayGame! Starting with default values");
            Input = new MinigameInput(1, 1, 1, null);
        }

        if (Input.BDifficulty > Input.ADifficulty)
        {
            busAnswer = Answer.A;
            bus.GetComponentInChildren<Text>().text = "A";
            bus.tag = "answerA";

            carAnswer = Answer.B;
            car.GetComponentInChildren<Text>().text = "B";
            car.tag = "answerB";

            busAnimator.speed = speed + 0.1f * Input.ADifficulty;
            carAnimator.speed = speed + 0.1f * Input.BDifficulty;
        }
        else
        {
            busAnswer = Answer.B;
            bus.GetComponentInChildren<Text>().text = "B";
            bus.tag = "AnswerB";

            carAnswer = Answer.A;
            car.GetComponentInChildren<Text>().text = "A";
            car.tag = "AnswerA";

            busAnimator.speed = speed + 0.1f * Input.BDifficulty;
            carAnimator.speed = speed + 0.1f * Input.ADifficulty;
        }

        busAnimator.speed += 0.1f * Input.TimeScale;
        carAnimator.speed += 0.1f * Input.TimeScale;

        Debug.Log("HighwayGame: CarSpeed=" + carAnimator.speed + " BusSpeed=" + busAnimator.speed);
    }

    public override void BeginGame()
    {
        this.enabled = true;
        busAnimator.enabled = true;
        carAnimator.enabled = true;

    }

    public void AnswerClicked()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        bus.GetComponent<Button>().interactable = false;
        car.GetComponent<Button>().interactable = false;

        if (clicked == bus)
        {
            busAnimator.enabled = false;
            Finish(busAnswer);
        }
        else if(clicked == car)
        {
            carAnimator.enabled = false;
            Finish(carAnswer);
        }
    }
}
