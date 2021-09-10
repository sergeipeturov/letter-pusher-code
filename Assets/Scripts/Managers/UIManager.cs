using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject LevelStartPanel;
    public GameObject LevelCompletePanel;
    public GameObject GameCompletePanel;
    public GameObject DebugPanel;

    public GameObject MainMenuPanel;
    public GameObject LevelChoisePanel;
    public GameObject ResetPanel;

    public GameObject[] LevelButtons;

    public void ShowMainMenu(bool show)
    {
        if (show)
            MainMenuPanel.SetActive(true);
        else
            MainMenuPanel.SetActive(false);
    }
    public void ShowResetPanel(bool show)
    {
        if (show)
            ResetPanel.SetActive(true);
        else
            ResetPanel.SetActive(false);
    }
    public void ShowLevelChoisePanel(bool show)
    {
        if (show)
        {
            foreach(var level in GameObject.Find("LevelManager").GetComponent<LevelManager>().Levels)
            {
                var buttonObj = LevelButtons[level.LevelNumber - 1];
                buttonObj.GetComponent<Button>().enabled = level.Unlocked;
                if (level.Unlocked)
                {
                    buttonObj.GetComponent<Image>().color = Color.white;
                    buttonObj.transform.Find("Text").GetComponent<Text>().color = Color.white;
                }
                else
                {
                    buttonObj.GetComponent<Image>().color = Color.gray;
                    buttonObj.transform.Find("Text").GetComponent<Text>().color = Color.gray;
                }
            }
            LevelChoisePanel.SetActive(true);
        }
        else
        {
            LevelChoisePanel.SetActive(false);
        }
    }

    public void ShowLevelStartPanel(string text)
    {
        LevelStartPanel.transform.Find("Text").GetComponent<Text>().text = text;
        LevelStartPanel.SetActive(true);
    }
    public void ShowLevelCompletePanel(int newRecord)
    {
        string newRecordString = newRecord == 0 ? "" : $"New Best Moves = {newRecord}!";
        LevelCompletePanel.transform.Find("Text").GetComponent<Text>().text = $"Congratulations!{Environment.NewLine}Level Completed!{Environment.NewLine}{newRecordString}";
        LevelCompletePanel.SetActive(true);
    }

    public void ShowGameCompletePanel()
    {
        GameCompletePanel.transform.Find("Text").GetComponent<Text>().text = $"Congratulations!{Environment.NewLine}All Levels Completed!{Environment.NewLine}You Win!";
        GameCompletePanel.SetActive(true);
    }

    public void OnGoButtonClick()
    {
        LevelStartPanel.SetActive(false);
    }
    public void OnNextButtonClick()
    {
        LevelCompletePanel.SetActive(false);
        GameObject.Find("LevelManager").GetComponent<LevelManager>().StartNextLevel();
    }

    public void SetDebugText(string text)
    {
        DebugPanel.transform.Find("Text").GetComponent<Text>().text = text;
    }
}
