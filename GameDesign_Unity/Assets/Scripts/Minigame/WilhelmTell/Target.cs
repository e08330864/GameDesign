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
    private List<Ring3D> ring = new List<Ring3D>();
    private WilhelmTell wilhelmTell = null;
    private RectTransform shootingRangeRT = null;

    private int level = -1;
    private Vector2 startPosition;
    private Vector2 stopPosition;
    private Vector3 velocity;
    private Sprite hitRingSprite;
    private int targetSize;
    private int numberOfTargetRings;
    private float targetSpeed;
    private float targetZOffset;
    private float offsetTargetRings;
    private bool hitCheckInProcess = false;
    public bool HitCheckInProcess
    {
        get => hitCheckInProcess;
        set => hitCheckInProcess = value;
    }
    private bool isHit = false;

    private bool isInstanziated = false;

    // Start is called before the first frame update
    void Start()
    {
        if ((shootingRangeRT = transform.parent.transform.GetComponent<RectTransform>()) == null)
        {
            Debug.LogError("shootingRangeRT is NULL in Target");
        }
        if ((wilhelmTell = transform.parent.GetComponent<WilhelmTell>()) == null)
        {
            Debug.LogError("wilhelmTell is NULL in Target");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInstanziated)
        {
            // instanziate if SetParameter was called an level was set
            if (level != -1)
            {
                transform.localPosition = new Vector3(startPosition.x, startPosition.y, -targetZOffset);
                for (int i = numberOfTargetRings; i > 0; i--)
                {
                    Ring3D ring = Instantiate(ringPrefab);
                    ring.label = i;
                    MeshRenderer mr = ring.transform.GetComponent<MeshRenderer>();
                    mr.material = (i % 2 == 1) ? red : black;
                    ring.gameObject.transform.SetParent(gameObject.transform, false);
                    ring.gameObject.transform.localPosition = new Vector3(0f, 0f, -offsetTargetRings * (numberOfTargetRings - i));
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
            if (level != -1)
            {
                // movement of target
                transform.Translate(velocity * Time.deltaTime);
                //Debug.Log("target position=" + transform.localPosition);
                // destroy object if totaly outside area of shooting range
                if (transform.localPosition.x > (shootingRangeRT.rect.width + targetSize) / 2.0f ||
                    transform.localPosition.y > (shootingRangeRT.rect.height + targetSize) / 2.0f ||
                    transform.localPosition.x < -(shootingRangeRT.rect.width + targetSize) / 2.0f ||
                    transform.localPosition.y < -(shootingRangeRT.rect.width + targetSize) / 2.0f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void SetParameters(int level,
                              float targetZOffset,
                              Vector2 startPosition,
                              Vector2 stopPosition,
                              int targetSize = 100,
                              int numberOfTargetRings = 4,
                              float offsetTargetRings = 0.02f,
                              float targetSpeed = 1.0f)
    {
        this.level = level;
        this.targetZOffset = targetZOffset;
        this.startPosition = startPosition;
        this.stopPosition = stopPosition;
        this.velocity = (stopPosition - startPosition).normalized * targetSpeed;
        this.targetSize = targetSize;
        this.numberOfTargetRings = numberOfTargetRings;
        this.offsetTargetRings = offsetTargetRings;
        this.targetSpeed = targetSpeed;
    }

    public bool ringHitCounts(Ring3D ring)
    {
        if (isHit)
        {
            return false;
        }
        ring.transform.GetComponent<MeshRenderer>().material = yellow;
        isHit = true;
        wilhelmTell.Score += numberOfTargetRings - ring.Label + 1;
        Debug.Log("hit score=" + wilhelmTell.Score);
        return true;
    }
}
