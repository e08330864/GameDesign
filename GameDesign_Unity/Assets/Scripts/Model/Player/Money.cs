using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    private TextMeshProUGUI moneyPanelStatus;

    [SerializeField]
    private int value = 0;

    public int Value { get => value;  }

    void Start()
    {
        moneyPanelStatus = GameObject.FindGameObjectWithTag("MoneyPanelValue").GetComponent<TextMeshProUGUI>();
        SetValue(value);
    }

    public void SetValue(int value)
    {
        this.value = value;
        moneyPanelStatus.text = value.ToString() + " €";
    }
}
