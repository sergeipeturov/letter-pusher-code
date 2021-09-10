using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public LayerMask LayerMaskForCanMove;
    public LayerMask LayerMaskForMoveOnField;

    public string LetterName;
    public List<Vector3> Moves { get; set; } = new List<Vector3>();

    public void SetLetterName(string newName)
    {
        LetterName = newName;
        GetComponent<SpriteRenderer>().sprite = GameObject.Find("SpritesManager").GetComponent<SpritesManager>().GetSpriteByName(LetterName);
    }

    public bool Move(Vector3 from, Vector3 to)
    {
        if (CanMove(from, to) || isUndoing)
        {
            transform.position = to;
            string fieldLetter = MoveOnField(to);
            if (fieldLetter != "")
            {
                int teleportNum = 0;
                if (int.TryParse(fieldLetter, out teleportNum))
                {

                }
                else
                {
                    MoveOnRightField(fieldLetter);
                }
            }
                
            
            if (!isUndoing)
            {
                Moves.Add(from);
                GameObject.Find("UndoingManager").GetComponent<UndoingManager>().MoveNumberMovableObject.Add(
                    new MoveNumberMovableObject(GameObject.Find("LevelManager").GetComponent<LevelManager>().CurrentLevel.CurrentMove, this));
            }
            else
            {
                isUndoing = false;
            }
            return true;
        }
        return false;
    }

    public bool Undo()
    {
        if (Moves.Any())
        {
            isUndoing = true;
            Move(transform.position, Moves.Last());
            Moves.RemoveAt(Moves.Count - 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool MoveLeft()
    {
        Vector3 lastPosition = transform.position;
        Vector3 newPosotion = new Vector3(transform.position.x - 1, transform.position.y);
        return Move(lastPosition, newPosotion);
    }
    public virtual bool MoveRight()
    {
        Vector3 lastPosition = transform.position;
        Vector3 newPosotion = new Vector3(transform.position.x + 1, transform.position.y);
        return Move(lastPosition, newPosotion);
    }
    public virtual bool MoveUp()
    {
        Vector3 lastPosition = transform.position;
        Vector3 newPosotion = new Vector3(transform.position.x, transform.position.y + 1);
        return Move(lastPosition, newPosotion);
    }
    public virtual bool MoveDown()
    {
        Vector3 lastPosition = transform.position;
        Vector3 newPosotion = new Vector3(transform.position.x, transform.position.y - 1);
        return Move(lastPosition, newPosotion);
    }

    private bool CanMove(Vector3 from, Vector3 to)
    {
        //проверка если объект игрока препятствует (для случая, когда есть Player2)
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject.transform.position.x == to.x && playerObject.transform.position.y == to.y)
            return false;

        //проверка остальных препятствий
        GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(from, to, LayerMaskForCanMove);
        GetComponent<BoxCollider2D>().enabled = true;
        if (hit.transform == null)
        {
            return true;
        }
        else
        {
            if (hit.transform.gameObject.tag != "Wall")
            {
                if (hit.transform.gameObject.tag == "Letter")
                {
                    if (hit.transform.gameObject.GetComponent<Letter>().LetterName == "A")
                    {
                        if (hit.transform.gameObject.GetComponent<MovableObject>().CanMove(to, to + (to - from)))
                            return true;
                        else
                            return false;
                    }
                    if (hit.transform.gameObject.GetComponent<MovableObject>().Move(to, to + (to - from)))
                        return true;
                }
                else
                {
                    return true;
                }
            }
                
        }
        return false;
    }

    private string MoveOnField(Vector3 from)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(from, Vector3.back, LayerMaskForMoveOnField);
        GetComponent<BoxCollider2D>().enabled = true;

        if (hit.transform == null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            return "";
        }
        else
        {
            if (hit.transform.gameObject.tag == "Field")
            {
                return hit.transform.gameObject.GetComponent<Field>().LetterName;
            }
            if (hit.transform.gameObject.tag == "Teleport")
            {
                return hit.transform.gameObject.GetComponent<Teleport>().ColorNum.ToString();
            }

        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        return "";
    }

    private bool MoveOnRightField(string fieldLetter)
    {
        string curLetterName = "";
        if (gameObject.tag == "Player")
            curLetterName = "A";
        else
            curLetterName = gameObject.GetComponent<Letter>().LetterName;
        string fieldLetterName = fieldLetter; //hit.transform.gameObject.GetComponent<Field>().LetterName;
        fieldLetterName = fieldLetterName.ToUpper();
        if (curLetterName == fieldLetterName)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            return true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            return false;
        }
    }

    /*private bool MoveOnTeleport(string fieldLetter)
    {
        if (gameObject.tag == "Player")
        {

        }
            curLetterName = "A";
        else
            curLetterName = gameObject.GetComponent<Letter>().LetterName;
        string fieldLetterName = fieldLetter; //hit.transform.gameObject.GetComponent<Field>().LetterName;
        fieldLetterName = fieldLetterName.ToUpper();
        if (curLetterName == fieldLetterName)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            return true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            return false;
        }
    }*/

    private bool isUndoing = false;
}

/*public class Move
{
    public Vector3 Position { get; private set; }
    public bool IsTeleport { get; private set; }

    public Move(Vector3 position, bool isTeleport = false)
    {
        Position = position; IsTeleport = isTeleport;
    }

    public void SetMoveAsTeleport()
    {
        IsTeleport = true;
    }
}*/
