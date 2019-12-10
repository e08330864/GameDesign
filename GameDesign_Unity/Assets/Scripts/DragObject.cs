using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragObject : MonoBehaviour
{
	private Vector3 mOffset;
	private float mZCoord;
	private float depth = 10.0f;
	public Camera cam;
	private float maxHeight;
	private float verschieb =550;

	private Collider2D m_ObjectCollider;

    void Start()
    {
        //Fetch the GameObject's Collider (make sure they have a Collider component)
        //Here the GameObject's Collider is not a trigger
		if(cam == null)
		{
			 cam = Camera.main;
		}
        m_ObjectCollider = GetComponent<Collider2D>();
        //Output whether the Collider is a trigger type Collider or not
        Debug.Log("Trigger On : " + m_ObjectCollider.isTrigger);
    }
	
	void FixedUpdate()
	{
	var rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
	var targetPosition = new Vector2(0, rawPosition.y + verschieb);
	GetComponent<Rigidbody2D>().MovePosition(targetPosition);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
    
        Debug.Log("Triggered");
        GameObject fhand = collider.gameObject;
    
	}
	/*
	void Update ()

    {
		
         var mousePos = Input.mousePosition;
     
         var wantedPos = Camera.main.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, depth));
     
         transform.position = wantedPos;
    }*/

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

	void OnMouseDrag()
	{
			//transform.position= mousePosWorld + mOffset
			transform.position = GetMouseWorldPos() + mOffset;

			
	}


}
