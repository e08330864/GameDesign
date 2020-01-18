using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    public Storyboard storyboard;
    private Stress stress;
    private Money money;

    public void Start()
    {
        stress = FindObjectOfType<Stress>();
        money = FindObjectOfType<Money>();
    }

    public void afterHospitalMedicate()
    {
        stress.SetValue(1);
        money.SetValue(money.Value - 200);
        storyboard.GoNextButtonPressed();
    }

    public void afterHospitalSleep()
    {
        stress.SetValue(3);
        storyboard.GoNextButtonPressed();
    }
}
