using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyboard: MonoBehaviour {

    public List<Level> levels;
    public Transform levelParent;
    public int currentLevelIndex = -1;

    private GameObject currentLevel;

    public void Start()
    {
        SpawnNextLevel();
        SetPanelValues();
    }

    private void SetPanelValues()
    {
        FindObjectOfType<Energy>().SetValue(3);
        FindObjectOfType<Patience>().SetValue(3);
        FindObjectOfType<Lives>().SetValue(3);
    }

    public Level GetLevelByName(string name)
    {
        return levels.Find(e => e.name == name);
    }

    public void FinishLevel(Answer? answer)
    {
        if(answer != null)
            (levels[currentLevelIndex] as MinigameLevel).answer = answer.Value;
        SpawnNextLevel();
    }

    private void SpawnNextLevel()
    {
        currentLevelIndex++;
        if(currentLevel != null)
            GameObject.DestroyImmediate(currentLevel);
        if(currentLevelIndex < levels.Count)
            currentLevel = GameObject.Instantiate(levels[currentLevelIndex].prefab, levelParent);
    }
}
