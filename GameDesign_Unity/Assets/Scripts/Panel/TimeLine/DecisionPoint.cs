using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Character character = null;
    private string decisionText = "";
    private float stress;
    private float money;

    public DecisionPoint(Character character, string decisionText)
    {
        this.character = character;
        this.decisionText = decisionText;
    }

    private void Start()
    {
        stress = Object.FindObjectOfType<Stress>().Value;
        money = Object.FindObjectOfType<Money>().Value;
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
        gameObject.transform.Find("PersonIcon").GetComponent<Image>().sprite = character.GetSprite();
        gameObject.transform.Find("PersonName").GetComponent<TextMeshProUGUI>().text = character.characterName;
    }

    public void SetDecisionText(string decisionText)
    {
        this.decisionText = decisionText;
        gameObject.transform.Find("Decision").Find("DecisionBackground").Find("DecisionText").GetComponent<TextMeshProUGUI>().text = decisionText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.Find("Decision").gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.Find("Decision").gameObject.SetActive(false);
    }
}
