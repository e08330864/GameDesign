using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{

    [SerializeField]
    private int value = 0;

    public int Value { get => value; set => this.value = value; }

    void Start()
    {
        Value = value;
    }
}
