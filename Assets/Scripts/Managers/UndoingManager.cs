using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UndoingManager : MonoBehaviour
{
    public List<MoveNumberMovableObject> MoveNumberMovableObject { get; set; } = new List<MoveNumberMovableObject>();

    public void OnUndo()
    {
        if (MoveNumberMovableObject.Any())
        {
            int moveNum = MoveNumberMovableObject.Last().MoveNumber;
            while (MoveNumberMovableObject.Any() && MoveNumberMovableObject.Last().MoveNumber == moveNum)
            {
                MoveNumberMovableObject.Last().MovableObject.Undo();
                MoveNumberMovableObject.RemoveAt(MoveNumberMovableObject.Count - 1);
                GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.AttemptMove();
            }
        }
    }
}

public class MoveNumberMovableObject
{
    public int MoveNumber;
    public MovableObject MovableObject;

    public MoveNumberMovableObject(int moveNumber, MovableObject movableObject)
    {
        MoveNumber = moveNumber;
        MovableObject = movableObject;
    }
}
