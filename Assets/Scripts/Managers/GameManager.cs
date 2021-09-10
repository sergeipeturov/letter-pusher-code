using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState currentState;
    public GameState CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            OnGameStateChanged();
        }
    }

    void Start()
    {
        LoadGame();
        CurrentState = GameState.mainMenu;
    }

    public void OnLogoClick()
    {
        Application.OpenURL("https://vk.com/penguin_the_narrator");
    }

    public void OnExitClick()
    {
        SaveGame();
        Application.Quit();
    }

    public void OnResetClick()
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().ShowResetPanel(true);
    }

    public void OnPlayClick()
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().ShowLevelChoisePanel(true);
    }

    public void OnLevelButtonClick(int levelNum)
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().StartLevel(levelNum);
        CurrentState = GameState.playing;
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        LoadDefaultGameData();
        GameObject.Find("UIManager").GetComponent<UIManager>().ShowResetPanel(false);
    }

    private void OnGameStateChanged()
    {
        if (CurrentState == GameState.mainMenu)
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().ShowMainMenu(true);
        }
        if (CurrentState == GameState.playing)
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().ShowMainMenu(false);
            GameObject.Find("UIManager").GetComponent<UIManager>().ShowLevelChoisePanel(false);
        }
    }

    public void SaveGame()
    {
        DataToSaveLoad saveData = new DataToSaveLoad();
        foreach (var level in GameObject.Find("LevelManager").GetComponent<LevelManager>().Levels)
        {
            saveData.Levels.Add(level.ToLevelJson());
        }
        var saveJsonStr = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("SaveData", saveJsonStr);
    }

    private void LoadGame()
    {
        LoadDefaultGameData();
        if (PlayerPrefs.HasKey("SaveData"))
        {
            var loadJsonString = PlayerPrefs.GetString("SaveData");
            var loadData = JsonUtility.FromJson<DataToSaveLoad>(loadJsonString);
            if (loadData != null)
            {
                foreach (var level in GameObject.Find("LevelManager").GetComponent<LevelManager>().Levels)
                {
                    var levelToLoad = loadData.Levels.FirstOrDefault(x => x.LevelNumber == level.LevelNumber);
                    if (levelToLoad != null)
                        level.FromLevelJson(levelToLoad);
                }
            }
        }
    }

    private void LoadDefaultGameData()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().Levels = new List<Level>()
        {
            new Level() {LevelNumber = 1, LevelName = "Start", CurrentMove = 0, Unlocked = true },
            new Level() {LevelNumber = 2, LevelName = "Word", CurrentMove = 0 },
            new Level() {LevelNumber = 3, LevelName = "Sokoban", CurrentMove = 0 },
            new Level() {LevelNumber = 4, LevelName = "Trap", CurrentMove = 0 },
            new Level() {LevelNumber = 5, LevelName = "Portal", CurrentMove = 0 },
            new Level() {LevelNumber = 6, LevelName = "Family", CurrentMove = 0 },
            new Level() {LevelNumber = 7, LevelName = "Go On", CurrentMove = 0 },
            new Level() {LevelNumber = 8, LevelName = "Wind Rose", CurrentMove = 0 },
            new Level() {LevelNumber = 9, LevelName = "Step", CurrentMove = 0 },
            new Level() {LevelNumber = 10, LevelName = "Paradox", CurrentMove = 0 },
            new Level() {LevelNumber = 11, LevelName = "Mirror", CurrentMove = 0 },
            new Level() {LevelNumber = 12, LevelName = "Clone", CurrentMove = 0 },
            new Level() {LevelNumber = 13, LevelName = "Help", CurrentMove = 0 },
            new Level() {LevelNumber = 14, LevelName = "Happy", CurrentMove = 0 },
            new Level() {LevelNumber = 15, LevelName = "Love", CurrentMove = 0 },
            new Level() {LevelNumber = 16, LevelName = "Phone", CurrentMove = 0 },
            new Level() {LevelNumber = 17, LevelName = "Ring", CurrentMove = 0 },
            new Level() {LevelNumber = 18, LevelName = "My Way", CurrentMove = 0 },
            new Level() {LevelNumber = 19, LevelName = "Move", CurrentMove = 0 },
            new Level() {LevelNumber = 20, LevelName = "Razor", CurrentMove = 0 },
            new Level() {LevelNumber = 21, LevelName = "Hammer", CurrentMove = 0 },
            new Level() {LevelNumber = 22, LevelName = "Crossword", CurrentMove = 0 },
            new Level() {LevelNumber = 23, LevelName = "Game", CurrentMove = 0 },
            new Level() {LevelNumber = 24, LevelName = "Difficult", CurrentMove = 0 },
            new Level() {LevelNumber = 25, LevelName = "The End", CurrentMove = 0 },
        };
    }
}

public enum GameState : int
{
    mainMenu = 0,
    playing
}