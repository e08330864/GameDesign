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

    private float speed = 0.3f;
    private float busSpeed;
    private float carSpeed;

    private void Awake()
    {
        if (Input == null)
        {
            //Debug.LogError("MinigameInput not set for HighwayGame! Starting with default values");
            Input = new MinigameInput(1, 1, 1, null);
        }
        if (Input.BDifficulty > Input.ADifficulty)
        {
            busAnswer = Answer.A;
            bus.GetComponentInChildren<Text>().text = "A";
            carAnswer = Answer.B;
            car.GetComponentInChildren<Text>().text = "B";

            busAnimator.speed = speed + 0.1f * Input.ADifficulty;
            carAnimator.speed = speed + 0.1f * Input.BDifficulty;
        }
        else
        {
            busAnswer = Answer.B;
            bus.GetComponentInChildren<Text>().text = "B";
            carAnswer = Answer.A;
            car.GetComponentInChildren<Text>().text = "A";

            busAnimator.speed = speed + 0.1f * Input.BDifficulty;
            carAnimator.speed = speed + 0.1f * Input.ADifficulty;
        }

        busAnimator.speed += 0.1f * Input.TimeScale;
        carAnimator.speed += 0.1f * Input.TimeScale;
    }

    void Start()
    {

        timeLimit = timeLimit - (3.0f * Input.TimeScale);
        timeLeft = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void BeginGame()
    {
        this.enabled = true;
    }

    public void AnswerClicked()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        if (clicked == bus)
            Finish(busAnswer);
        else if(clicked == car)
            Finish(carAnswer);
    }
}
