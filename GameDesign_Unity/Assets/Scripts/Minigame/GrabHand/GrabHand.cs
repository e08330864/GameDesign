using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHand : MinigameController
{
    public GameObject aAnswerObject;
    public GameObject bAnswerObject;

    private Animator fhandAnim;

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
        fhandAnim.speed = speed + 0.1f * yesDifficulty;
        StartCoroutine(AnswerOnAnimationFinish());
    }

	private IEnumerator AnswerOnAnimationFinish()
    {
        yield return new WaitUntil(() => fhandAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        var noneAnswer = new Answer(AnswerValue.None, silentTimelineText, silentDeltas);
        FinishLevel(noneAnswer);
    }


	public override void StartLevel()
    {
        fhandAnim.enabled = true;
    }
	public void Answered(){

		

	    fhandAnim.enabled = false;
		/*
		if()
		{
		FinishLevel(yes);
		}
		

		else 
		{
		FinishLevel(no);
		}
		
    */
	
	}
}
