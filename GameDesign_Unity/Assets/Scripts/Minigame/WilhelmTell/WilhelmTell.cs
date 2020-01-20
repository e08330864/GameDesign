using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WilhelmTell : MonoBehaviour
{
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private Target targetPrefab = null;
    [SerializeField]
    private int numberOfTargets = 1;
    [SerializeField]
    private int targetSize = 800;
    [SerializeField]
    private int numberOfTargetRings = 3;
    [SerializeField]
    private float targetSpeed = 3.0f;
    [SerializeField]
    private Texture2D crossHairTexture = null;
    public Texture2D CrossHair
    {
        get => crossHairTexture;
    }
    [SerializeField]
    private Arrow arrowPrefab = null;
    [SerializeField]
    private int numberOfArrows = 3;
    public int NumberOfArrows
    {
        set => numberOfArrows = value;
        get => numberOfArrows;
    }

    private ShootingRange shootingRange = null;
    private ArrowsArea arrowsArea = null;
    private List<Target> targets = new List<Target>();
    private List<Arrow> arrows = new List<Arrow>();
    private Vector3 sizeInScreen;
    private Crossbow crossbow = null;
 
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
        // targets
        for (int i = 0; i < numberOfTargets; i++)
        {
            Target target = Instantiate(targetPrefab);
            target.transform.SetParent(this.transform, false);
            target.SetParameters(level, 
                new Vector2(200, 200), 
                new Vector2(200, 200), 
                targetSize, 
                numberOfTargetRings, 
                targetSpeed);
            targets.Add(target);
        }
        // arrows
        for (int i = 0; i < numberOfArrows; i++)
        {
            Arrow arrow = Instantiate(arrowPrefab);
            arrow.transform.SetParent(this.transform, false);
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x + i * 20,
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
