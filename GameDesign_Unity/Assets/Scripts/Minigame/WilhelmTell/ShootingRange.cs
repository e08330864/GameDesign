using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingRange : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Texture2D crossHairTexture = null;
    [SerializeField]
    private Crossbow crossbow = null;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 cursorHotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        cursorHotSpot = new Vector2(crossHairTexture.width / 2, crossHairTexture.height / 2);
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            crossbow.SetTarget(Input.mousePosition);
        }
    }

    //-------------------------------------------------------------------------------------------
    // setting the mouse curser to crosshair in case the mouse is within WT area and crossbow is loaded
    public void OnPointerEnter(PointerEventData pointer)
    {
        //Debug.Log("bin drin!!!");
        SetCurserInShootingRange();
    }

    public void OnPointerExit(PointerEventData pointer)
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        crossbow.ShootArrow();
    }

    public void SetCurserInShootingRange()
    {
        if (crossbow.CrossbowIsLoaded)
        {
            Cursor.SetCursor(crossHairTexture, cursorHotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
