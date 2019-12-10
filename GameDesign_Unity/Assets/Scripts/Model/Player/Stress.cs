using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour
{
    [SerializeField]
    private List<ValueBar> valueBars = new List<ValueBar>();

    [SerializeField]
    private int value = 0;

    public int Value { get => value; }

    void Start()
    {
        SetValue(value);
    }

    public void SetValue(int value)
    {
        this.value = value;
        foreach (ValueBar valueBar in valueBars)
        {
            valueBar.SetValue(value);
        }
    }

    public void ApplyDelta(int delta)
    {
        SetValue(value + delta);
    }

}
