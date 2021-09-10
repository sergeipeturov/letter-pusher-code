using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int LevelNumber { get; set; }
    public string LevelName { get; set; }
    public int MovesRecord { get; set; }
    
    private int currentMove;
    public int CurrentMove { get { return currentMove; } 
        set { currentMove = value; 
            AttemptMove();
            GameObject.Find("UIManager").GetComponent<UIManager>().SetDebugText($"Level = {LevelNumber} | Move = {CurrentMove} | Best Moves = {MovesRecord}"); } }

    private int numberOfFields;
    private int numberOfRightLetters;
    public int NumberOfFields { get { return numberOfFields; } 
        set { numberOfFields = value; GameObject.Find("UIManager").GetComponent<UIManager>().SetDebugText($"Level = {LevelNumber} | Move = {CurrentMove} | Best Moves = {MovesRecord}"); } }
    public int NumberOfRightLetters { get { return numberOfRightLetters; }
        set { numberOfRightLetters = value; GameObject.Find("UIManager").GetComponent<UIManager>().SetDebugText($"Level = {LevelNumber} | Move = {CurrentMove} | Best Moves = {MovesRecord}"); } }

    public List<MovableObject> Fields { get; set; } = new List<MovableObject>();
    public List<MovableObject> Letters { get; set; } = new List<MovableObject>();
    public List<Teleport> Teleports { get; set; } = new List<Teleport>();

    public bool Unlocked { get; set; } = false;

    public Level()
    {
        NumberOfFields = 0;
        NumberOfRightLetters = 0;
        Fields.Clear();
        Letters.Clear();
        Teleports.Clear();
    }

    public void ResetLevel()
    {
        CurrentMove = 0;
        NumberOfFields = 0;
        NumberOfRightLetters = 0;
        Fields.Clear();
        Letters.Clear();
        Teleports.Clear();
    }

    public void AttemptMove()
    {
        if (CurrentMove > 0)
        {
            //проверка телепортов
            foreach (var teleport in Teleports)
            {
                if (GameObject.Find("GameManagers").GetComponent<ControlManager>().Player.gameObject.transform.position.x == teleport.gameObject.transform.position.x &&
                    GameObject.Find("GameManagers").GetComponent<ControlManager>().Player.gameObject.transform.position.y == teleport.gameObject.transform.position.y)
                {
                    int telNum = teleport.ColorNum;
                    switch(telNum)
                    {
                        case 1:
                            var targetTeleportGameObject1 = Teleports.FirstOrDefault(x => x.ColorNum == 2).gameObject;
                            if (IsTargetTeleportFree(targetTeleportGameObject1))
                                GameObject.Find("GameManagers").GetComponent<ControlManager>().Player.gameObject.transform.position = 
                                    new Vector3(targetTeleportGameObject1.transform.position.x, targetTeleportGameObject1.transform.position.y, 0.0f);
                            break;
                        case 2:
                            var targetTeleportGameObject2 = Teleports.FirstOrDefault(x => x.ColorNum == 1).gameObject;
                            if (IsTargetTeleportFree(targetTeleportGameObject2))
                                GameObject.Find("GameManagers").GetComponent<ControlManager>().Player.gameObject.transform.position =
                                    new Vector3(targetTeleportGameObject2.transform.position.x, targetTeleportGameObject2.transform.position.y, 0.0f);
                            break;
                        case 3:
                            var targetTeleportGameObject3 = Teleports.FirstOrDefault(x => x.ColorNum == 4).gameObject;
                            if (IsTargetTeleportFree(targetTeleportGameObject3))
                                GameObject.Find("GameManagers").GetComponent<ControlManager>().Player.gameObject.transform.position = 
                                    new Vector3(targetTeleportGameObject3.transform.position.x, targetTeleportGameObject3.transform.position.y, 0.0f);
                            break;
                        case 4:
                            var targetTeleportGameObject4 = Teleports.FirstOrDefault(x => x.ColorNum == 3).gameObject;
                            if (IsTargetTeleportFree(targetTeleportGameObject4))
                                GameObject.Find("GameManagers").GetComponent<ControlManager>().Player.gameObject.transform.position = 
                                    new Vector3(targetTeleportGameObject4.transform.position.x, targetTeleportGameObject4.transform.position.y, 0.0f);
                            break;
                        case 5:
                            var targetTeleportGameObject5 = Teleports.FirstOrDefault(x => x.ColorNum == 6).gameObject;
                            if (IsTargetTeleportFree(targetTeleportGameObject5))
                                GameObject.Find("GameManagers").GetComponent<ControlManager>().Player.gameObject.transform.position = 
                                    new Vector3(targetTeleportGameObject5.transform.position.x, targetTeleportGameObject5.transform.position.y, 0.0f);
                            break;
                        case 6:
                            var targetTeleportGameObject6 = Teleports.FirstOrDefault(x => x.ColorNum == 5).gameObject;
                            if (IsTargetTeleportFree(targetTeleportGameObject6))
                                GameObject.Find("GameManagers").GetComponent<ControlManager>().Player.gameObject.transform.position = 
                                    new Vector3(targetTeleportGameObject6.transform.position.x, targetTeleportGameObject6.transform.position.y, 0.0f);
                            break;
                    }
                    break;
                }

            }

            //проверка полей и букв
            NumberOfRightLetters = 0;
            NumberOfFields = Fields.Count;
            foreach (var field in Fields)
            {
                var letter = Letters.FirstOrDefault(x => x.gameObject.transform.position.x == field.transform.position.x && x.gameObject.transform.position.y == field.gameObject.transform.position.y);
                if (letter != null)
                {
                    if (letter.LetterName == field.LetterName.ToUpper())
                    {
                        NumberOfRightLetters++;
                        letter.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                        //break;
                    }
                    else
                    {
                        letter.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        //break;
                    }
                }

                /*foreach(var letter in Letters)
                {
                    if (letter.gameObject.transform.position.x == field.gameObject.transform.position.x &&
                        letter.gameObject.transform.position.y == field.gameObject.transform.position.y &&
                        letter.LetterName == field.LetterName.ToUpper())
                    {
                        NumberOfRightLetters++;
                        letter.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                        break;
                    }
                    else
                    {
                        if (Fields.Any(x => x.gameObject.transform.position.x == letter.transform.position.x && x.gameObject.transform.position.y == letter.transform.position.y))
                            letter.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        else
                            letter.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }*/
            }

            if (NumberOfRightLetters == NumberOfFields)
                GameObject.Find("LevelManager").GetComponent<LevelManager>().CompleteLevel();
        }
    }

    public LevelJson ToLevelJson()
    {
        var lvlJson = new LevelJson()
        {
            LevelNumber = LevelNumber,
            MovesRecord = MovesRecord,
            Unlocked = Unlocked
        };
        return lvlJson;
    }

    public void FromLevelJson(LevelJson levelJson)
    {
        MovesRecord = levelJson.MovesRecord;
        Unlocked = levelJson.Unlocked;
    }

    private bool IsTargetTeleportFree(GameObject targetTeleportGameObject)
    {
        var targetTeleportPosition = targetTeleportGameObject.transform.position;
        targetTeleportGameObject.GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(targetTeleportPosition, Vector3.forward, 1);
        targetTeleportGameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (hit.transform != null && hit.transform.gameObject.tag == "Letter" &&
            hit.transform.position.x == targetTeleportPosition.x &&
            hit.transform.position.y == targetTeleportPosition.y)
            return false;
        else
            return true;
    }
}

[Serializable]
public class LevelJson
{
    public int LevelNumber;
    public int MovesRecord;
    public bool Unlocked; 
}