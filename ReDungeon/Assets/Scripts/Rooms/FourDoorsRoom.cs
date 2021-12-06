using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDoorsRoom : Room
{
    public enum Positions { LeftUp, LeftDown, RightUp, RightDown }
    public enum Doors { U, R, D, L }

    public Door DoorU;
    public Door DoorR;
    public Door DoorD;
    public Door DoorL;

    [SerializeField]
    private Positions position;
    public Positions Position
    {
        get => position;
        set => position = value;
    }
    public void SetRandomPosition()
    {
        int x = Random.Range(0, 3);
        Position = (Positions)x;
    }
    public Door OpenDoor(Sides side)
    {
        switch (side)
        {
            case Sides.Top:
                DoorU.door.SetActive(false);
                DoorU.IsDoorOpened = true;
                return DoorU;
            case Sides.Right:
                DoorR.door.SetActive(false);
                DoorR.IsDoorOpened = true;
                return DoorR;
            case Sides.Bottom:
                DoorD.door.SetActive(false);
                DoorD.IsDoorOpened = true;
                return DoorD;
            case Sides.Left:
                DoorL.door.SetActive(false);
                DoorL.IsDoorOpened = true;
                return DoorL;
            default:
                return null;
        }
    }
    public void OpenTwoDifferentDoors(Sides side)
    {
        int x, y;
        while (true)
        {
            x = Random.Range(0, 4);
            y = Random.Range(0, 4);
            if (x != (int)side && y != (int)side && x != y) break;
        }
        OpenDoor((Sides)x);
        OpenDoor((Sides)y);
    }
    public Door GetDoor(Sides side)
    {
        switch (side)
        {
            case Sides.Top: return DoorU;
            case Sides.Right: return DoorR;
            case Sides.Bottom: return DoorD;
            case Sides.Left: return DoorL;
            default: return null;
        }
    }
}
