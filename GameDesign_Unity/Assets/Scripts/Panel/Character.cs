using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    private Image image = null;
    private TextMeshProUGUI sympathyValue = null;

    public bool hasBorrowedMoney = false;
    public string characterName = "";
    [SerializeField]
    private float playerSympathy = 3;
    [SerializeField]
    private float maxSympathy = 5;
    [SerializeField]
    private float minSympathy = 0;

    void Start()
    {
        if ((image = gameObject.transform.Find("Image").gameObject.GetComponent<Image>()) == null)
        {
            Debug.LogError("image is NULL in Character");
        }
        if ((sympathyValue = gameObject.transform.Find("Image").Find("Value").gameObject.GetComponent<TextMeshProUGUI>()) == null)
        {
            Debug.LogError("value is NULL in Character");
        }
        sympathyValue.text = playerSympathy.ToString();
    }

    public void applySympathyDelta(float delta)
    {
        playerSympathy = Mathf.Min(maxSympathy, Mathf.Max(minSympathy, playerSympathy + delta));
        sympathyValue.text = playerSympathy.ToString();

    }

    public float GetPlayerSympathy()
    {
        return playerSympathy;
    }

    public Sprite GetFigureImage()
    {
        return image.sprite;
    }
}
