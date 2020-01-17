using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Target target = null;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Quarrel")
        {
            if (target.ringHitCounts(this))
            {
                IsHit = true;
            }
        }
    }
}
