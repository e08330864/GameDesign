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
    private Text tPrice;
    private Text tPercentage;

    private void Start()
    {
        if ((tName = transform.Find("Name").GetComponent<Text>()) == null)
        {
            Debug.Log("tName is NULL in JitterReducer");
        }
        if ((tPrice = transform.Find("Price").GetComponent<Text>()) == null)
        {
            Debug.Log("tPrice is NULL in JitterReducer");
        }
        if ((tPercentage = transform.Find("Percentage").GetComponent<Text>()) == null)
        {
            Debug.Log("tPercentage is NULL in JitterReducer");
        }
        UpdateText();
    }

    private void UpdateText()
    {
        tName.text = name;
        tPrice.text = "Preis: " + price.ToString() + " €";
        tPercentage.text = $"Um {percentageReduction}% ruhigere Hände";
    }

    public void activateDrug()
    {
        var sb = FindObjectOfType<Storyboard>();
        sb.currentJitterReduction = percentageReduction;
        sb.money.SetValue(sb.money.Value-price);
        sb.GoNextButtonPressed();
        this.GetComponent<Button>().enabled = false;
    }
}
