﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Target target = null;
    public string label = "";

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

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision object = " + collision.name + "  label=" + label);
        if (collision.name == "Arrow(Clone)")
        {
            //if (target.ringHitCounts(this))
            //{
            //    IsHit = true;
            //}
        }
        Debug.Log("hit = " + IsHit);
    }
}
