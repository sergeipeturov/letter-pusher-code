using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int ColorNum; //1,2 - желтый, 3,4 - голубой, 5,6 - пурпурный

    public void SetTeleportNum(int num)
    {
        ColorNum = num;
        switch(num)
        {
            case 1:
            case 2:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 3:
            case 4:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 5:
            case 6:
                GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
        }
        
    }
}
