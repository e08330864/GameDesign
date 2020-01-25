using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LendMoney : MonoBehaviour
{
    public Text boss;
    public Text vanessa;
    public Text caroline;
    public Text john;

    public Character bossChar;
    public Character vanessaChar;
    public Character carolineChar;
    public Character johnChar;

    private float bossAmount;
    private float vanessaAmount;
    private float carolineAmount;
    private float johnAmount;

    public void Init()
    {
        bossAmount = setupChar(bossChar, boss);
        vanessaAmount = setupChar(vanessaChar, vanessa);
        carolineAmount = setupChar(carolineChar, caroline);
        johnAmount = setupChar(johnChar, john);
    }

    private float setupChar(Character ch, Text text)
    {
        var inScene = GameObject.FindGameObjectWithTag(ch.tag);
        float amount = 0;
        if (inScene != null)
        {
            text.gameObject.SetActive(true);
            amount = inScene.GetComponent<Character>().GetPlayerSympathy() * 50;
            text.text = ""+amount+"€ ausborgen.";
            if (inScene.GetComponent<Character>().hasBorrowedMoney)
            {
                text.gameObject.GetComponent<Button>().interactable = false;
                text.text = "Hat dir schon etwas geborgt.";
            }
        }
        else
        {
            text.gameObject.SetActive(false);
        }

        return amount;
    }

    //Dumpster Fire
    public void lendMoney()
    {
        var selected = EventSystem.current.currentSelectedGameObject.GetComponent<Text>();
        var sb = FindObjectOfType<Storyboard>();

        if(selected == boss)
        {
            sb.money.SetValue(sb.money.Value + (int)bossAmount);
            GameObject.FindGameObjectWithTag(bossChar.tag).GetComponent<Character>().hasBorrowedMoney = true;
        }
        else if(selected == vanessa)
        {
            sb.money.SetValue(sb.money.Value + (int)vanessaAmount);
            GameObject.FindGameObjectWithTag(vanessaChar.tag).GetComponent<Character>().hasBorrowedMoney = true;

        }
        else if(selected == caroline)
        {
            sb.money.SetValue(sb.money.Value + (int)carolineAmount);
            GameObject.FindGameObjectWithTag(carolineChar.tag).GetComponent<Character>().hasBorrowedMoney = true;

        }
        else if(selected == john)
        {
            sb.money.SetValue(sb.money.Value + (int)johnAmount);
            GameObject.FindGameObjectWithTag(johnChar.tag).GetComponent<Character>().hasBorrowedMoney = true;

        }
        sb.GoNextButtonPressed();
    }

}
