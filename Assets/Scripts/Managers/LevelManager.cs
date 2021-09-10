using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> Levels { get; set; } = new List<Level>();
    //{
    //    new Level() {LevelNumber = 1, LevelName = "Start", CurrentMove = 0 },
    //    new Level() {LevelNumber = 2, LevelName = "Word", CurrentMove = 0 }
    //};

    public int CurrentLevelNumber { get; set; }

    public Level CurrentLevel { get { return Levels[CurrentLevelNumber - 1]; } }

    private void Start()
    {
    }

    public void StartLevel(int levelNum)
    {
        CurrentLevelNumber = levelNum;
        CurrentLevel.ResetLevel();
        GameObject.Find("FieldGenerator").GetComponent<FieldGenerator>().GenerateLevel(levelNum);
        GameObject.Find("GameManagers").GetComponent<ControlManager>().InitPlayer();
        GameObject.Find("UIManager").GetComponent<UIManager>().ShowLevelStartPanel($"Level {levelNum}{Environment.NewLine}{Levels[levelNum - 1].LevelName}");
        GameObject.Find("UIManager").GetComponent<UIManager>().SetDebugText($"Level = {CurrentLevel.LevelNumber} | Move = {CurrentLevel.CurrentMove} | Best Moves = {CurrentLevel.MovesRecord}");
    }

    public void StartNextLevel()
    {
        if (CurrentLevelNumber + 1 <= Levels.Count)
        {
            CurrentLevelNumber++;
            StartLevel(CurrentLevelNumber);
        }
        else
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().ShowGameCompletePanel();
        }
    }

    public void RestartLevel()
    {
        StartLevel(CurrentLevelNumber);
    }

    public void CompleteLevel()
    {
        Invoke("GoCompleteLevel", 0.5f);
    }

    private void GoCompleteLevel()
    {
        int newRecord = 0;
        if (CurrentLevel.CurrentMove < CurrentLevel.MovesRecord || CurrentLevel.MovesRecord == 0)
        {
            newRecord = CurrentLevel.CurrentMove;
            CurrentLevel.MovesRecord = newRecord;
        }

        if (CurrentLevelNumber + 1 <= Levels.Count)
        {
            Levels[CurrentLevelNumber].Unlocked = true;
        }
        GameObject.Find("GameManagers").GetComponent<GameManager>().SaveGame();
        GameObject.Find("UIManager").GetComponent<UIManager>().ShowLevelCompletePanel(newRecord);
    }
}
