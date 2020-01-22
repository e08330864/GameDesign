using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crossbow : MonoBehaviour
{
    [SerializeField]
    private bool crossbowIsLoaded = false;

    private Text instruction = null;
    public bool CrossbowIsLoaded
    {
        get => crossbowIsLoaded;
        set
        {
            crossbowIsLoaded = value;
            if (crossbowIsLoaded)
            {
                arrowsArea.SetRaycast(false);
                instruction.enabled = false;
            }
            else
            {
                arrowsArea.SetRaycast(true);
                instruction.enabled = true;
            }
        }
    }

    private ArrowsArea arrowsArea = null;
    private ShootingRange shootingRange = null;
    private Vector2 shootingRangeSizeDelta;
    private float bottomOffset = 0f;
    private float leftOffset = 0f;


    // Start is called before the first frame update
    void Start()
    {
        if ((shootingRange = GameObject.FindObjectOfType<ShootingRange>()) == null)
        {
            Debug.LogError("shootingRange is NULL in Crossbow");
        }
        if ((arrowsArea = GameObject.FindObjectOfType<ArrowsArea>()) == null)
        {
            Debug.LogError("arrowsArea is NULL in Crossbow");
        }
        if ((instruction = GameObject.Find("Instruction").GetComponent<Text>()) == null)
        {
            Debug.LogError("instruction is NULL in WilhelmTell");
        }
        leftOffset = ((RectTransform)shootingRange.transform).rect.width / 2.0f + transform.localPosition.x;
        bottomOffset = ((RectTransform)shootingRange.transform).rect.height / 2.0f + transform.localPosition.y + 250;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Vector3 targetPoint)
    {
        if (CrossbowIsLoaded)
        {
            transform.localEulerAngles = CalculateEulerAngelesTo(targetPoint);
        }
    }

    private Vector3 CalculateEulerAngelesTo(Vector3 target)
    {
        Vector3 localEulerAngles = new Vector3();
        float xAngle = Mathf.Atan((target.y - bottomOffset) / (target.z - transform.localPosition.z)) * Mathf.Rad2Deg;
        localEulerAngles.x = -90 - xAngle;
        float yAngle = Mathf.Atan((target.x - leftOffset) / (target.z - transform.localPosition.z)) * Mathf.Rad2Deg;
        localEulerAngles.y = yAngle;
        localEulerAngles.z = 90;
        return localEulerAngles;
    }

    public void ShootArrow()
    {
        if (CrossbowIsLoaded)
        {
            Arrow arrow = transform.Find("Arrow(Clone)").GetComponent<Arrow>();
            arrow.transform.localPosition = new Vector3(0f, 0f, 0f);
            CrossbowIsLoaded = false;
            arrow.Fly = true;
            shootingRange.SetCurserInShootingRange();
        }
    }
}
