using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WilhelmTell : MonoBehaviour
{
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private Target targetPrefab = null;
    [SerializeField]
    private int numberOfTargets = 1;
    [SerializeField]
    private int targetSize = 300;
    [SerializeField]
    private int numberOfTargetRings = 6;
    [SerializeField]
    private float targetSpeed = 3.0f;
    [SerializeField]
    private Arrow arrowPrefab = null;
    [SerializeField]
    private int stress = 0;
    [SerializeField]
    private float jitter = 4;

    //--------------------------------------------------------------
    private int numberOfArrows = 3;
    public int NumberOfArrows
    {
        set => numberOfArrows = value;
        get => numberOfArrows;
    }
    private int score = 0;
    public int Score
    {
        set
        {
            score = value;
            scoreValueText.text = score.ToString();
        }
        get => score;
    }
    //--------------------------------------------------------------

    private ShootingRange shootingRange = null;
    private RectTransform shootingRangeRT = null;
    private RectTransform thisRT = null;
    private ArrowsArea arrowsArea = null;
    private List<Target> targets = new List<Target>();
    private List<Arrow> arrows = new List<Arrow>();
    private Vector3 sizeInScreen;
    private Crossbow crossbow = null;
    private Text scoreValueText = null;
    private float offsetTargetRings = 0.02f;
    private Vector3 startPoint = Vector3.zero;
    private Vector3 stopPoint = Vector3.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        //sizeInScreen = transform.Find("ShootingRange").GetComponent<SpriteRenderer>().bounds.size; <-- User RectTransform for this
        
        if ((shootingRange = GameObject.FindObjectOfType<ShootingRange>()) == null)
        {
            Debug.LogError("shootingRange is NULL in WilhelmTell");
        }
        if ((arrowsArea = GameObject.FindObjectOfType<ArrowsArea>()) == null)
        {
            Debug.LogError("arrowsArea is NULL in WilhelmTell");
        }
        if ((crossbow = GameObject.FindObjectOfType<Crossbow>()) == null)
        {
            Debug.LogError("crossbow is NULL in WilhelmTell");
        }
        if ((scoreValueText = GameObject.Find("Score").GetComponent<Text>()) == null)
        {
            Debug.LogError("scoreValueText is NULL in WilhelmTell");
        }
        if ((shootingRangeRT = transform.Find("ShootingRange").transform.GetComponent<RectTransform>()) == null)
        {
            Debug.LogError("shootingRangeRT is NULL in WilhelmTell");
        }
        if ((thisRT = transform.GetComponent<RectTransform>()) == null)
        {
            Debug.LogError("thisRT is NULL in WilhelmTell");
        }
        shootingRange.Stress = stress;
        shootingRange.Jitter = jitter;
        // targets
        // randomly path
        bool fromLeft = (Random.Range(0,2) == 0) ? true : false;
        for (int i = 0; i < numberOfTargets; i++)
        {
            fromLeft = !fromLeft;   // flip fly direction
            // random start and stop point
            startPoint.y = Random.Range(0, shootingRangeRT.rect.height) - shootingRangeRT.rect.height / 2;
            stopPoint.y = Random.Range(0, shootingRangeRT.rect.height) - shootingRangeRT.rect.height / 2;
            if (fromLeft)
            {
                startPoint.x = -(shootingRangeRT.rect.width + targetSize) / 2;
                stopPoint.x = (shootingRangeRT.rect.width + targetSize) / 2;
            }
            else
            {
                startPoint.x = (shootingRangeRT.rect.width + targetSize) / 2;
                stopPoint.x = -(shootingRangeRT.rect.width + targetSize) / 2;
            }
            //Debug.Log("from=" + startPoint + "  to=" + stopPoint);
            Target target = Instantiate(targetPrefab);
            target.transform.SetParent(this.transform, false);
            target.SetParameters(level,
                i * numberOfTargetRings * offsetTargetRings,
                startPoint,
                stopPoint, 
                targetSize, 
                numberOfTargetRings,
                offsetTargetRings,
                targetSpeed);
            targets.Add(target);
        }
        // arrows
        for (int i = 0; i < numberOfArrows; i++)
        {
            Arrow arrow = Instantiate(arrowPrefab);
            arrow.transform.SetParent(arrowsArea.gameObject.transform, false);
            arrow.transform.localPosition = new Vector3(20 + arrow.transform.localPosition.x + i * 20,
                arrow.transform.localPosition.y,
                this.transform.localPosition.z + 0.001f);
            arrows.Add(arrow);
        }
        arrowsArea.SetWidth(arrows.Count);
    }

    public void LoadCrossbow()
    {
        if (arrows.Count > 0 && !crossbow.CrossbowIsLoaded)
        {
            // reset crossbow rotation
            crossbow.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);
            // remove arrow from stock
            Destroy(arrows[arrows.Count - 1].gameObject);
            arrows.RemoveAt(arrows.Count - 1);
            // create arrow in crossbow
            Arrow arrow = Instantiate(arrowPrefab);
            arrow.transform.SetParent(crossbow.transform, false);
            arrow.transform.localPosition = new Vector3(0f, 0f, 0.1f);
            arrow.transform.localScale = Vector3.one;
            arrow.transform.localEulerAngles = new Vector3(arrow.transform.eulerAngles.x, arrow.transform.eulerAngles.y, 0);
            crossbow.CrossbowIsLoaded = true;
            // change cursor
            shootingRange.SetCurserInShootingRange();
        }
    }

    public int GetNumArrowsOnStock()
    {
        return arrows.Count;
    }
}
