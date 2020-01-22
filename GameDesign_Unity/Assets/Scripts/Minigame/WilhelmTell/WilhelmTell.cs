using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//--------------------------------------------------------------------------
// The function of a 100% bull-eyes missel arrow is not implemented
//
// Input values (for the first naive implementation):
//  
//      Stress              ... current stress level - must be taken from player
//      SumberOfTargets     ... defines the number of targets, depending on difficulty - for size and number of rings the standard parameters can be used
//      NumberOfArrows
//      JitterPercentage    ... Percentage factor of final Jitter (result of jitter reduction by jitter reduction tools)
//
//  Return VAlues
//
//      Score               ... reached score 
//      MaximumScore        ... maximumScore = numberOfTargets * (numberOfTargetRings * 10)
//      
//
//  There is also a draft of a JitterShop as prescene/cutscene for the training/turnament, in which you can buy the drugs
//  100% bulls-eye arrow and continous valid crossbow weights fare not implemented in WT, so only the drucgs should be working in the first prototyp
//
//  Desired functionality in jitter shop: 
//      
//      +/- buttons for a jitter item to buy - whereby you only can buy up to the money you have
//      the bought items are automatically valid for the upcoming training/tournament --> JitterPercentage
//          (of course, if borrowing functionality is working related to persons likes, it would be great)
//
//  The effect of the reached score should be communicated afterwards and be calculated according following formula:
//      delta_stress = Ceil( Score / (maximumScore / maximumStress) )
//
//  In the final turnament (with at least 4 targets and only 4 arrows), the score to be reached (otherwise game lost) should be given for example like this:
//      scoreThreshold = 0.8 * maximumScore



public class WilhelmTell : MonoBehaviour
{
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private Target targetPrefab = null;
    [SerializeField]
    private int numberOfTargets = 1;
    public int NumberOfTargets
    {
        set => NumberOfTargets = value;
        get => numberOfTargets;
    }
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
    public float Stress
    {
        set => Stress = value;
        get => Stress;
    }

    [SerializeField]
    private float jitter = 4;
    [SerializeField]
    private float jitterPercentage = 1f;  // the total calculatet jitter is multiplied by this factor, therefore it is the percentage of jitter
    public float JitterPercentage
    {
        set => jitterPercentage = value;
        get => jitterPercentage;
    }

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
    public int MaximumScore
    {
        get => NumberOfTargets * numberOfTargetRings;
    }
    //--------------------------------------------------------------
    public int currentArrowCount;
    public Text effects;

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
        stress = FindObjectOfType<Storyboard>().stress.Value;
        JitterPercentage = FindObjectOfType<Storyboard>().currentJitterReduction;
        jitter = jitter * 1/jitterPercentage;
        if(jitterPercentage > 0)
        {
            effects.text = "Aktuelle Effekte: \n" + $"{FindObjectOfType<Storyboard>().currentJitterReduction}% ruhigere Hände.";
        }
        else
        {
            effects.text = "Aktuelle Effekte: \n Keine.";
        }

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
        shootingRange.Jitter = jitter * JitterPercentage;
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
        currentArrowCount = numberOfArrows;
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

    public void arrowShot()
    {
        currentArrowCount--;
        Debug.Log($"Du hast {score} Punkte von {MaximumScore} Punkten erreicht!\n\n");
        if (currentArrowCount == 0)
        {
            var sb = FindObjectOfType<Storyboard>();
            sb.currentJitterReduction = 0;
            sb.nextButton.SetActive(true);
            var txt = sb.trainingResultOverlay.GetComponentInChildren<Text>();
            txt.text = $"Du hast {score} Punkte von {MaximumScore} Punkten erreicht!\n\n";
            if (Score / (float)MaximumScore >= 0.80f)
            {
                txt.text += "Du bist überglücklich mit deinem Ergebnis und fühlst dich weniger gestresst.";
                sb.stress.ApplyDelta(-1);
            }
            else if(Score / (float)MaximumScore >= 0.7f)
            {
                txt.text += "Du bist zufrieden mit deinem Ergebnis. \n";
            }
            else
            {
                txt.text += "Du bist nicht zufrieden mit deinem Ergebnis. \n Beim Gedanken ans Bezirksturnier fühlst du dich gestresst...";
                sb.stress.ApplyDelta(1);
            }

        }
    }
}
