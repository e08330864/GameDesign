using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField]
    private List<LiveSmiles> liveSmiles = new List<LiveSmiles>();

    [SerializeField]
    private int value = 3;

    void Start()
    {
        SetValue(value);
    }

    public void SetValue(int value)
    {
        this.value = value;
        foreach (LiveSmiles ls in liveSmiles)
        {
            ls.SetValue(value);
        }
    }
}
