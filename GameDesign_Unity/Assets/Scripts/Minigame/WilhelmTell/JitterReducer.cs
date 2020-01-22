using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JitterReducer : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string number;
    [SerializeField]
    private int price = 0;
    [SerializeField]
    private int percentageReduction = 0;
    public int PercentageReduction
    {
        get => percentageReduction;
        set => percentageReduction = Mathf.Max(0, Mathf.Min(100, value));
    }
    [SerializeField]
    private bool once = true;

    private bool isUsed = false;
    private Text tName;
    private Text tNumber;
    private Text tPrice;
    private Text tPercentage;
    private Text tOnce;

    // returns the multiplication factor for the jitter according to the defined percentage reduction
    public float useReducer()
    {
        if (!isUsed)
        {
            if (once)
            {
                isUsed = true;
            }
            return (100 - PercentageReduction) / 100f;
        }
        else
        {
            return 1f;
        }
    }

    private void Start()
    {
        if ((tName = transform.Find("Name").GetComponent<Text>()) == null)
        {
            Debug.Log("tName is NULL in JitterReducer");
        }
        if ((tNumber = transform.Find("Number").GetComponent<Text>()) == null)
        {
            Debug.Log("tNumber is NULL in JitterReducer");
        }
        if ((tPrice = transform.Find("Price").GetComponent<Text>()) == null)
        {
            Debug.Log("tPrice is NULL in JitterReducer");
        }
        if ((tPercentage = transform.Find("Percentage").GetComponent<Text>()) == null)
        {
            Debug.Log("tPercentage is NULL in JitterReducer");
        }
        if ((tOnce = transform.Find("Once").GetComponent<Text>()) == null)
        {
            Debug.Log("tOnce is NULL in JitterReducer");
        }
        UpdateText();
    }

    private void Update()
    {
        
    }

    private void UpdateText()
    {
        tName.text = name;
        tNumber.text = "Number: " + number.ToString() + " pcs.";
        tPrice.text = "Price: " + price.ToString() + " €";
        tPercentage.text = "Jitter percentage reduction: -" + percentageReduction.ToString() + "%";
        tOnce.text = "Usage: " + ((once) ? "once" : "continous");
    }
}
