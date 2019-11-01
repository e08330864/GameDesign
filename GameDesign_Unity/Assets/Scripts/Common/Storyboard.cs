using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyboard: MonoBehaviour {

    public List<Level> gameEvents;
    public Transform levelParent;

    private GameObject currentLevel;
    private int currentLevelIndex = -1;

    public void Start()
    {
        SpawnNextLevel();
    }

    public Level GetEventByName(string name)
    {
        return gameEvents.Find(e => e.name == name);
    }

    public void FinishLevel(Answer? answer)
    {
        if(answer != null)
            (gameEvents[currentLevelIndex] as MinigameLevel).answer = answer.Value;
        SpawnNextLevel();
    }

    private void SpawnNextLevel()
    {
        currentLevelIndex++;
        if(currentLevel != null)
            GameObject.DestroyImmediate(currentLevel);
        currentLevel = GameObject.Instantiate(gameEvents[currentLevelIndex].prefab, levelParent);
    }
}
