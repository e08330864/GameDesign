using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverOverlay : MonoBehaviour
{
    public Text gameOverText;

    public void GameOver(string text)
    {
        this.gameObject.SetActive(true);
        gameOverText.text = text;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
