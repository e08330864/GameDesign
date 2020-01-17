using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingRange : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private WilhelmTell wilhelmTell = null;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        if ((wilhelmTell = transform.parent.GetComponent<WilhelmTell>()) == null)
        {
            Debug.Log("wilhelmTell is NULL in ShootingRange");
        }    
    }

    //-------------------------------------------------------------------------------------------
    // setting the mouse curser to crosshair in case the mouse is within WT area and crossbow is loaded
    public void OnPointerEnter(PointerEventData pointer)
    {
        Debug.Log("bin drin!!!");
        Cursor.SetCursor(wilhelmTell.CrossHair, hotSpot, cursorMode);
    }

    public void OnPointerExit(PointerEventData pointer)
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
