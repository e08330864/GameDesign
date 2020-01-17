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
    private Sprite bullEyeSprite = null;
    [SerializeField]
    private Sprite secondRingSprite = null;
    [SerializeField]
    private Sprite hitRingSprite = null;
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

    private List<Target> targets = new List<Target>();
    private Vector3 sizeInScreen;


    // Start is called before the first frame update
    void Start()
    {
        sizeInScreen = transform.Find("ShootingRange").GetComponent<SpriteRenderer>().bounds.size;
        for (int i = 0; i < numberOfTargets; i++)
        {
            Target target = Instantiate(targetPrefab);
            target.transform.SetParent(this.transform, false);
            target.SetParameters(level, 
                new Vector2(200, 200), 
                new Vector2(200, 200), 
                bullEyeSprite, 
                secondRingSprite,
                hitRingSprite,
                targetSize, 
                numberOfTargetRings, 
                targetSpeed);
            targets.Add(target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(EventSystem.current.IsPointerOverGameObject());
    }
}
