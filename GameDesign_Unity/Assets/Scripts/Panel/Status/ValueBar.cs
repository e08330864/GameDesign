using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueBar : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> valueBars_Prefab = new List<GameObject>();
    private List<GameObject> valueBars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject vbp in valueBars_Prefab)
        {
            GameObject valueBar =  Instantiate(vbp, transform);
            valueBar.SetActive(false);
            valueBars.Add(valueBar);
        }
    }

    public void SetValue(int value)
    {
        value = value < 0 ? 0 : value > valueBars.Count ? valueBars.Count : value;
        foreach (GameObject vb in valueBars)
        {
            vb.SetActive(false);
        }
        if (value > 0)
        {
            valueBars[value - 1].SetActive(true);
        }
    }
}
