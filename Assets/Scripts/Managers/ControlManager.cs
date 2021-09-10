using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public Player Player;
    public Letter Player2;

    public void InitPlayer()
    {
        Player = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player")).Last().GetComponent<Player>();
        //Player.gameObject.transform.localScale = new Vector3(Player.gameObject.transform.localScale.x + 0.05f, Player.gameObject.transform.localScale.y + 0.05f, Player.gameObject.transform.localScale.z); //для теста
        var letterA = GameObject.FindGameObjectsWithTag("Letter").LastOrDefault(x => x.GetComponent<Letter>().LetterName == "A");
        if (letterA == null)
            Player2 = null;
        else
            Player2 = letterA.GetComponent<Letter>();
        //letters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Letter"));
    }

    public void DestroyPlayer()
    {
        if (Player != null)
        {
            Destroy(Player.gameObject);
            Player = null;
        }
    }

    public void OnLeft()
    {
        if (Player2 != null)
        {
            if (Player.gameObject.transform.position.x < Player2.gameObject.transform.position.x)
            {
                playerMoved = Player.MoveLeft();
                player2Moved = Player2.MoveLeft();
            }
            else
            {
                player2Moved = Player2.MoveLeft();
                playerMoved = Player.MoveLeft();
            }
        }
        else
        {
            playerMoved = Player.MoveLeft();
        }
        if (playerMoved || player2Moved)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove++;
    }
    public void OnRight()
    {
        if (Player2 != null)
        {
            if (Player.gameObject.transform.position.x > Player2.gameObject.transform.position.x)
            {
                playerMoved = Player.MoveRight();
                player2Moved = Player2.MoveRight();
            }
            else
            {
                player2Moved = Player2.MoveRight();
                playerMoved = Player.MoveRight();
            }
        }
        else
        {
            playerMoved = Player.MoveRight();
        }
        if (playerMoved || player2Moved)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove++;
    }
    public void OnUp()
    {
        if (Player2 != null)
        {
            if (Player.gameObject.transform.position.y > Player2.gameObject.transform.position.y)
            {
                playerMoved = Player.MoveUp();
                player2Moved = Player2.MoveUp();
            }
            else
            {
                player2Moved = Player2.MoveUp();
                playerMoved = Player.MoveUp();
            }
        }
        else
        {
            playerMoved = Player.MoveUp();
        }
        if (playerMoved || player2Moved)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove++;
    }
    public void OnDown()
    {
        if (Player2 != null)
        {
            if (Player.gameObject.transform.position.y < Player2.gameObject.transform.position.y)
            {
                playerMoved = Player.MoveDown();
                player2Moved = Player2.MoveDown();
            }
            else
            {
                player2Moved = Player2.MoveDown();
                playerMoved = Player.MoveDown();
            }
        }
        else
        {
            playerMoved = Player.MoveDown();
        }
        if (playerMoved || player2Moved)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove++;
    }

    public void OnQuitLevel()
    {
        GameObject.Find("GameManagers").GetComponent<GameManager>().CurrentState = GameState.mainMenu;
    }

    public void OnResetLevel()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().RestartLevel();
    }

    //private List<GameObject> letters = new List<GameObject>();
    private bool playerMoved = false;
    private bool player2Moved = false;
}
