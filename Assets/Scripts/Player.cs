using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovableObject
{
    public override bool MoveLeft()
    {
        lastPosition = transform.position;
        newPosotion = new Vector3(transform.position.x - 1, transform.position.y);
        //if (Move(lastPosition, newPosotion))
        //    GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove++;
        return Move(lastPosition, newPosotion);
    }
    public override bool MoveRight()
    {
        lastPosition = transform.position;
        newPosotion = new Vector3(transform.position.x + 1, transform.position.y);
        //if (Move(lastPosition, newPosotion))
        //    GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove++;
        return Move(lastPosition, newPosotion);
    }
    public override bool MoveUp()
    {
        lastPosition = transform.position;
        newPosotion = new Vector3(transform.position.x, transform.position.y + 1);
        //if (Move(lastPosition, newPosotion))
        //    GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove++;
        return Move(lastPosition, newPosotion);
    }
    public override bool MoveDown()
    {
        lastPosition = transform.position;
        newPosotion = new Vector3(transform.position.x, transform.position.y - 1);
        //if (Move(lastPosition, newPosotion))
        //    GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove++;
        return Move(lastPosition, newPosotion);
    }

    private Vector3 lastPosition;
    private Vector3 newPosotion;
}
