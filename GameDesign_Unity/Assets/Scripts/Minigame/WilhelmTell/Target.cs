using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Material black = null;
    [SerializeField]
    private Material red = null;
    [SerializeField]
    private Material yellow = null;
    [SerializeField]
    private Ring3D ringPrefab = null;
    private List<Ring3D> rings = new List<Ring3D>();
    private WilhelmTell wilhelmTell = null;

    private int level = -1;
    private Vector2 startPosition;
    private Vector2 stopPosition;
    private Vector3 velocity;
    //private Sprite bullEyeSprite;
    //private Sprite secondRingSprite;
    private Sprite hitRingSprite;
    private int targetSize;
    private int numberOfTargetRings;
    private float targetSpeed;
    private bool hitCheckInProcess = false;
    public bool HitCheckInProcess
    {
        get => hitCheckInProcess;
        set => hitCheckInProcess = value;
    }
    private List<int> hitRank = new List<int>(); 

    private bool isInstanziated = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInstanziated)
        {
            if (level != -1)
            {
                wilhelmTell = gameObject.transform.parent.parent.GetComponent<WilhelmTell>();
                transform.localPosition = new Vector3(startPosition.x, startPosition.y, 0);
                for (int i = numberOfTargetRings; i > 0; i--)
                {
                    Ring3D ring = Instantiate(ringPrefab);
                    ring.label = i;
                    MeshRenderer mr = ring.transform.GetComponent<MeshRenderer>();
                    mr.material = (i % 2 == 1) ? red : black;
                    ring.gameObject.transform.SetParent(gameObject.transform, false);
                    ring.gameObject.transform.localPosition = new Vector3(0f, 0f, -0.05f * (numberOfTargetRings - i));
                    float scale = targetSize / (numberOfTargetRings * 2 - 1) * (i * 2 - 1);
                    //Debug.Log("rect-width=" + img.sprite.rect.width);
                    Debug.Log("ring " + i + " scale=" + scale);
                    ring.gameObject.transform.localScale = new Vector3(scale, 0.01f, scale);
                }
                isInstanziated = true;
            }
        }
        else
        {
            // movement of target

        }
    }

    public void SetParameters(int level,
                              Vector2 startPosition,
                              Vector2 stopPosition,
                              int targetSize = 100, 
                              int numberOfTargetRings = 4, 
                              float targetSpeed = 1.0f)
    {
        this.level = level;
        this.startPosition = startPosition;
        this.stopPosition = stopPosition;
        this.velocity = (stopPosition - startPosition).normalized * targetSpeed;
        this.targetSize = targetSize;
        this.numberOfTargetRings = numberOfTargetRings;
        this.targetSpeed = targetSpeed;
    }

    public bool ringHitCounts(Ring3D ring)
    {
        hitRank.Add(ring.label);
        StartCoroutine(WaitForOthers());
        // check for smalest label
        int smallestLabel = hitRank[0];
        foreach (int i in hitRank)
        {
            smallestLabel = (smallestLabel < i) ? smallestLabel : i;
        }
        if (ring.label != smallestLabel)
        {
            return false;
        }
        bool validHit = true;
        foreach (Ring3D r in rings)
        {
            if (r.IsHit)
            {
                validHit = false;
                break;
            }
        }
        if (validHit)
        {
            ring.IsHit = true;
            MeshRenderer mr = ring.transform.GetComponent<MeshRenderer>();
            mr.material = yellow;
        }
        hitRank.Clear();
        return validHit;
    }

    IEnumerator WaitForOthers()
    {
        yield return new WaitForSeconds(2f);
    }
}
