using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    public int MinX;
    public int MaxX;
    public int MinY;
    public int MaxY;

    public TextAsset[] Levels;

    public void GenerateLevel(int levelNum)
    {
        //очистка от предыдущего уровня
        List<GameObject> walls = new List<GameObject>(GameObject.FindGameObjectsWithTag("Wall"));
        List<GameObject> fields = new List<GameObject>(GameObject.FindGameObjectsWithTag("Field"));
        List<GameObject> letters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Letter"));
        List<GameObject> teleports = new List<GameObject>(GameObject.FindGameObjectsWithTag("Teleport"));
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach (var item in walls)
        {
            if (item.GetComponent<Wall>().DestroyOnLoad)
                Destroy(item);
        }
        foreach (var item in fields)
            Destroy(item);
        foreach (var item in letters)
            Destroy(item);
        foreach (var item in teleports)
            Destroy(item);
        GameObject.Find("GameManagers").GetComponent<ControlManager>().DestroyPlayer();

        //генерация текущего уровня
        string levelField = Levels[levelNum - 1].text;
        levelField = levelField.Replace('\r', '-');
        levelField = levelField.Replace('\n', '-');
        levelField = levelField.Replace("--", "");
        int levelFieldIndex = 0;
        for (int j = MinY; j >= MaxY; j--)
        {
            for (int i = MinX; i <= MaxX; i++)
            {
                var pref = GetPrefab(levelField[levelFieldIndex]);
                if (pref != null)
                {
                    int z = (pref.tag == "Field" || pref.tag == "Teleport") ? -1 : 0;
                    var obj = Instantiate(pref, new Vector3(i, j, z), Quaternion.identity);
                    if (pref.tag == "Field")
                        //GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.NumberOfFields++;
                        GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.Fields.Add(obj.GetComponent<MovableObject>());
                    if (pref.tag == "Letter" || pref.tag == "Player")
                        //GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.NumberOfFields++;
                        GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.Letters.Add(obj.GetComponent<MovableObject>());
                    if (pref.tag == "Teleport")
                        GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.Teleports.Add(obj.GetComponent<Teleport>());
                }
                levelFieldIndex++;
            }
        }
    }

    private GameObject GetPrefab(char symbol)
    {
        var pm = GameObject.Find("PrefabsManager").GetComponent<PrefabsManager>();
        switch (symbol)
        {
            case '@':
                GameObject player = pm.Player;
                player.GetComponent<Player>().SetLetterName("A");
                return player;
                //return pm.Player;
            case '#': 
                return pm.Wall;
            case 'A':
            case 'B':
            case 'C':
            case 'D':
            case 'E':
            case 'F':
            case 'G':
            case 'H':
            case 'I':
            case 'J':
            case 'K':
            case 'L':
            case 'M':
            case 'N':
            case 'O':
            case 'P':
            case 'Q':
            case 'R':
            case 'S':
            case 'T':
            case 'U':
            case 'V':
            case 'W':
            case 'X':
            case 'Y':
            case 'Z':
                GameObject letter = pm.Letter;
                letter.GetComponent<Letter>().SetLetterName(symbol.ToString());
                return letter;
            case 'a':
            case 'b':
            case 'c':
            case 'd':
            case 'e':
            case 'f':
            case 'g':
            case 'h':
            case 'i':
            case 'j':
            case 'k':
            case 'l':
            case 'm':
            case 'n':
            case 'o':
            case 'p':
            case 'q':
            case 'r':
            case 's':
            case 't':
            case 'u':
            case 'v':
            case 'w':
            case 'x':
            case 'y':
            case 'z':
                GameObject field = pm.Field;
                field.GetComponent<Field>().SetLetterName(symbol.ToString());
                return field;
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
                GameObject teleport = pm.Teleport;
                teleport.GetComponent<Teleport>().SetTeleportNum(int.Parse(symbol.ToString()));
                return teleport;
            default:
                return null;
        }
    }
}
