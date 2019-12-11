using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private Character character = null;

    private Image image = null;
    private TextMeshProUGUI value = null;

    // Start is called before the first frame update
    void Start()
    {
        if ((image = gameObject.transform.Find("Image").gameObject.GetComponent<Image>()) == null)
        {
            Debug.LogError("image is NULL in Characterstatus");
        }
        if ((value = gameObject.transform.Find("Image").Find("Value").gameObject.GetComponent<TextMeshProUGUI>()) == null)
        {
            Debug.LogError("value is NULL in Characterstatus");
        }
        image.sprite = character.GetFigureImage();
        value.text = character.GetPlayerSympathy().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        value.text = character.GetPlayerSympathy().ToString();
    }
}
