using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{
    public Sprite let_A;
    public Sprite let_B;
    public Sprite let_C;
    public Sprite let_D;
    public Sprite let_E;
    public Sprite let_F;
    public Sprite let_G;
    public Sprite let_H;
    public Sprite let_I;
    public Sprite let_J;
    public Sprite let_K;
    public Sprite let_L;
    public Sprite let_M;
    public Sprite let_N;
    public Sprite let_O;
    public Sprite let_P;
    public Sprite let_Q;
    public Sprite let_R;
    public Sprite let_S;
    public Sprite let_T;
    public Sprite let_U;
    public Sprite let_V;
    public Sprite let_W;
    public Sprite let_X;
    public Sprite let_Y;
    public Sprite let_Z;

    public Sprite field_A;
    public Sprite field_B;
    public Sprite field_C;
    public Sprite field_D;
    public Sprite field_E;
    public Sprite field_F;
    public Sprite field_G;
    public Sprite field_H;
    public Sprite field_I;
    public Sprite field_J;
    public Sprite field_K;
    public Sprite field_L;
    public Sprite field_M;
    public Sprite field_N;
    public Sprite field_O;
    public Sprite field_P;
    public Sprite field_Q;
    public Sprite field_R;
    public Sprite field_S;
    public Sprite field_T;
    public Sprite field_U;
    public Sprite field_V;
    public Sprite field_W;
    public Sprite field_X;
    public Sprite field_Y;
    public Sprite field_Z;

    public Sprite Dot;
    public Sprite Wall;

    public Sprite GetSpriteByName(string letterName)
    {
        switch(letterName)
        {
            case "A": return let_A;
            case "B": return let_B;
            case "C": return let_C;
            case "D": return let_D;
            case "E": return let_E;
            case "F": return let_F;
            case "G": return let_G;
            case "H": return let_H;
            case "I": return let_I;
            case "J": return let_J;
            case "K": return let_K;
            case "L": return let_L;
            case "M": return let_M;
            case "N": return let_N;
            case "O": return let_O;
            case "P": return let_P;
            case "Q": return let_Q;
            case "R": return let_R;
            case "S": return let_S;
            case "T": return let_T;
            case "U": return let_U;
            case "V": return let_V;
            case "W": return let_W;
            case "X": return let_X;
            case "Y": return let_Y;
            case "Z": return let_Z;

            case "a": return field_A;
            case "b": return field_B;
            case "c": return field_C;
            case "d": return field_D;
            case "e": return field_E;
            case "f": return field_F;
            case "g": return field_G;
            case "h": return field_H;
            case "i": return field_I;
            case "j": return field_J;
            case "k": return field_K;
            case "l": return field_L;
            case "m": return field_M;
            case "n": return field_N;
            case "o": return field_O;
            case "p": return field_P;
            case "q": return field_Q;
            case "r": return field_R;
            case "s": return field_S;
            case "t": return field_T;
            case "u": return field_U;
            case "v": return field_V;
            case "w": return field_W;
            case "x": return field_X;
            case "y": return field_Y;
            case "z": return field_Z;

            case "#": return Wall;
            default: return Dot;
        }
    }
}
