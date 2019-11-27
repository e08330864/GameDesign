using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public TextMeshProUGUI yesAnswer;
    public TextMeshProUGUI noAnswer;
    public TextMeshProUGUI question;
    public TextMeshProUGUI personName;
    public Image personImage;

    private GameObject conversationContainer = null;
    private GameObject timelineContainer = null;

    private void Start()
    {
        if ((conversationContainer = gameObject.transform.Find("ConversationContainer").gameObject) == null)
        {
            Debug.LogError("conversationContainer is NULL in Panel");
        }
        if ((timelineContainer = gameObject.transform.Find("TimeLineContainer").gameObject) == null)
        {
            Debug.LogError("timelineContainer is NULL in Panel");
        }
        timelineContainer.SetActive(true);
        conversationContainer.SetActive(false);
    }

    public void ActivateConversation()
    {
        timelineContainer.SetActive(false);
        conversationContainer.SetActive(true);
    }

    public void ActivateTimeLine()
    {
        timelineContainer.SetActive(true);
        conversationContainer.SetActive(false);
    }
}
