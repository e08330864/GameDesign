using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring3D : MonoBehaviour
{
    private Target target = null;
    public int label;
    public int Label
    {
        get => label;
        set => label = value;
    }

    private bool ishit = false;
    public bool IsHit
    {
        get => ishit;
        set => ishit = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        if ((target = transform.parent.GetComponent<Target>()) == null)
        {
            Debug.LogError("target is NULL in Ring");
        }
    }

    public void hit()
    {
        if (!IsHit)
        {
            IsHit = target.ringHitCounts(this);
        }
    }
}
