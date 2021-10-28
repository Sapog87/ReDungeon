using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Sides { Top, Right, Bottom, Left }

    public static Sides GetOppositeSide(Sides side)
    {
        switch (side)
        {
            case Sides.Top: return Sides.Bottom;
            case Sides.Right: return Sides.Left;
            case Sides.Bottom: return Sides.Top;
            case Sides.Left:return Sides.Right;
            default: throw new System.Exception();
        }
    }
    //Список тунелей
    //TODO
}
