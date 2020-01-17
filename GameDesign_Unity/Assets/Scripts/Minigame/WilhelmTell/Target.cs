using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Ring ringPrefab = null;
    private List<Ring> rings = new List<Ring>();
    private WilhelmTell wilhelmTell = null;

    private int level = -1;
    private Vector2 startPosition;
    private Vector2 stopPosition;
    private Vector3 velocity;
    private Sprite bullEyeSprite;
    private Sprite secondRingSprite;
    private Sprite hitRingSprite;
    private int targetSize;
    private int numberOfTargetRings;
    private float targetSpeed;

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
                    Ring ring = Instantiate(ringPrefab);
                    Image img = ring.gameObject.GetComponent<Image>();
                    img.sprite = (i % 2 == 1) ? bullEyeSprite : secondRingSprite;
                    ring.gameObject.transform.SetParent(gameObject.transform, false);
                    ring.gameObject.transform.localPosition = Vector3.zero;
                    RectTransform rt = (RectTransform)ring.transform;
                    float scale = targetSize / (numberOfTargetRings * 2 - 1) * (i * 2 - 1) / rt.rect.width;
                    //Debug.Log("rect-width=" + img.sprite.rect.width);
                    //Debug.Log("scale=" + scale);
                    ring.gameObject.transform.localScale = new Vector3(scale, scale, 1.0f);
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
                              Sprite bullEyeSprite,
                              Sprite secondRingSprite,
                              Sprite hitRingSprite,
                              int targetSize = 100, 
                              int numberOfTargetRings = 4, 
                              float targetSpeed = 1.0f)
    {
        this.level = level;
        this.startPosition = startPosition;
        this.stopPosition = stopPosition;
        this.velocity = (stopPosition - startPosition).normalized * targetSpeed;
        this.bullEyeSprite = bullEyeSprite;
        this.secondRingSprite = secondRingSprite;
        this.hitRingSprite = hitRingSprite;
        this.targetSize = targetSize;
        this.numberOfTargetRings = numberOfTargetRings;
        this.targetSpeed = targetSpeed;
    }

    public bool ringHitCounts(Ring ring)
    {
        bool validHit = true;
        foreach (Ring r in rings)
        {
            if (r.IsHit)
            {
                validHit = false;
                break;
            }
        }
        if (validHit)
        {
            ring.gameObject.GetComponent<Image>().sprite = hitRingSprite;
        }
        return validHit;
    }
}
