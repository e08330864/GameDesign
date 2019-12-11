using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    private GameObject timelineContainer = null;
    private GameObject StatusContainer = null;

    private void Start()
    {
        if ((timelineContainer = gameObject.transform.Find("TimeLineContainer").gameObject) == null)
        {
            Debug.LogError("timelineContainer is NULL in Panel");
        }
        timelineContainer.SetActive(true);
        if ((StatusContainer = gameObject.transform.Find("StatusContainer").gameObject) == null)
        {
            Debug.LogError("StatusContainer is NULL in Panel");
        }
        StatusContainer.SetActive(true);
    }
}
