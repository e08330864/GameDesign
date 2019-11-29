using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string characterName = "";
    public Sprite iconImage = null;
    public Sprite figureImage = null;
    private float playerSympathy = 3;
    private float maxSympathy = 5;
    private float minSympathy = 0;

    public void IncreasePlayerSympathy()
    {
        playerSympathy = Mathf.Min(maxSympathy, Mathf.Max(minSympathy, playerSympathy + 1));
    }

    public void DecreasePlayerSympathy()
    {
        playerSympathy = Mathf.Min(maxSympathy, Mathf.Max(minSympathy, playerSympathy - 1));
    }

    public float GetPlayerSympathy()
    {
        return playerSympathy;
    }
}
