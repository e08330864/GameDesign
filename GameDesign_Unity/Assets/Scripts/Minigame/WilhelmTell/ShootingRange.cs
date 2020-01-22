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
    private int stress = 0;
    public int Stress
    {
        set => stress = value;
    }
    private float jitter = 4;
    public float Jitter
    {
        set => jitter = value;
    }
    Vector3 tremble = Vector3.zero;
    Vector2 tremble2 = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        cursorHotSpot = new Vector2(crossHairTexture.width / 2, crossHairTexture.height / 2);
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            tremble = new Vector3(Random.Range(-1f, +1f) * stress * jitter,
                                  Random.Range(-1f, +1f) * stress * jitter,
                                  0f);
            SetCurserInShootingRange();
            crossbow.SetTarget(Input.mousePosition + tremble);
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
        tremble2 = tremble;
        if (crossbow.CrossbowIsLoaded)
        {
            Cursor.SetCursor(crossHairTexture, cursorHotSpot - tremble2, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
