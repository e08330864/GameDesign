using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowsArea : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Crossbow crossbow = null;

    private WilhelmTell wilhelmTell = null;
    private Image img = null;

    // Start is called before the first frame update
    void Start()
    {
        if ((wilhelmTell = transform.parent.GetComponent<WilhelmTell>()) == null)
        {
            Debug.Log("wilhelmTell is NULL in ArrowsArea");
        }
        img = transform.GetComponent<Image>();
    }
    
    public void SetWidth(int numArrows)
    {
        RectTransform rt = (RectTransform)this.transform;
        rt.sizeDelta = new Vector2(40 * (numArrows > 0 ? 1 : 0) + 20 * (numArrows - 1), rt.sizeDelta.y);
    }

    public void SetRaycast(bool value)
    {
        img.raycastTarget = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!crossbow.CrossbowIsLoaded)
        {
            wilhelmTell.LoadCrossbow();
            SetWidth(wilhelmTell.GetNumArrowsOnStock());
        }
    }
}
