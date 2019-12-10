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
	private Vector3 mOffset;
	private float mZCoord;
	private Collider2D m_ObjectCollider;


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
	    
     
	void Start()
    {
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        //Here the GameObject's Collider is not a trigger
        m_ObjectCollider = GetComponent<Collider2D>();
        //Output whether the Collider is a trigger type Collider or not
        Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);
    }
	void OnTriggerEnter2D(Collider2D collider)
	{
    
        Debug.Log("Triggered");
        GameObject fhand = collider.gameObject;
    
	}
	void OnMouseDown()
	{
			mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			
			//Store offset = gameobject world pos - mouse world posmOffset
			mOffset = gameObject.transform.position - GetMouseWorldPos();
			//GameObject's Collider is now a trigger Collider when the GameObject is clicked. It now acts as a trigger
			m_ObjectCollider.isTrigger = true;
			//Output to console the GameObject’s trigger state
			Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);
	}
	private Vector3 GetMouseWorldPos()
	{
		//pixel coordinates (x,y)
		Vector3 mousePoint = Input.mousePosition;

		// z coordinate of game object on screen
		mousePoint.z = mZCoord;

		return Camera.main.ScreenToWorldPoint(mousePoint);
		
	}
	


	public override void StartLevel()
    {
        fhandAnim.enabled = true;
    }
	public void Answered(){

		

	    fhandAnim.enabled = false;
		
		if(m_ObjectCollider.isTrigger == true)
		{
		var yes = new Answer(AnswerValue.YES, yesTimelineText, yesDeltas);
		FinishLevel(yes);
		}
		


	/*
		else 
		{
		FinishLevel(no);
		}
		
    */
	
	}
}
