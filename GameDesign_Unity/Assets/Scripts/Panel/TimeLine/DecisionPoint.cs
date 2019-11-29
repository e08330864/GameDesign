using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Character character = null;
    private Answer answer = null;
    private string gameShortText = "";

    private void Start()
    {
    }

    public void SetGameShortText(string gameShortText)
    {
        gameObject.transform.Find("GameShortText").GetComponent<TextMeshProUGUI>().text = gameShortText;
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
        gameObject.transform.Find("PersonIcon").GetComponent<Image>().sprite = character.iconImage;
        gameObject.transform.Find("PersonImage").GetComponent<Image>().sprite = character.figureImage;
        gameObject.transform.Find("PersonName").GetComponent<TextMeshProUGUI>().text = character.characterName;
    }

    public void SetAnswer(Answer answer)
    {
        this.answer = answer;
        gameObject.transform.Find("Decision").Find("DecisionBackground").Find("DecisionText").GetComponent<TextMeshProUGUI>().text = answer.timeLineText;
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
