using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightDoorsRoom : Room
{
    public enum Doors { UL, UR, RU, RD, DL, DR, LU, LD }

    public Door DoorUL;
    public Door DoorUR;
    public Door DoorRU;
    public Door DoorRD;
    public Door DoorDL;
    public Door DoorDR;
    public Door DoorLU;
    public Door DoorLD;
    public Door OpenDoor(Doors door)
    {
        switch (door)
        {
            case Doors.UL:
                DoorUL.door.SetActive(false);
                DoorUL.IsDoorOpened = true;
                return DoorUL;
            case Doors.UR:
                DoorUR.door.SetActive(false);
                DoorUR.IsDoorOpened = true;
                return DoorUR;
            case Doors.RU:
                DoorRU.door.SetActive(false);
                DoorRU.IsDoorOpened = true;
                return DoorRU;
            case Doors.RD:
                DoorRD.door.SetActive(false);
                DoorRD.IsDoorOpened = true;
                return DoorRD;
            case Doors.DL:
                DoorDL.door.SetActive(false);
                DoorDL.IsDoorOpened = true;
                return DoorDL;
            case Doors.DR:
                DoorDR.door.SetActive(false);
                DoorDR.IsDoorOpened = true;
                return DoorDR;
            case Doors.LU:
                DoorLU.door.SetActive(false);
                DoorLU.IsDoorOpened = true;
                return DoorLU;
            case Doors.LD:
                DoorLD.door.SetActive(false);
                DoorLD.IsDoorOpened = true;
                return DoorLD;
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
        OpenDoor((Doors)Random.Range(x * 2, (x + 1) * 2));
        OpenDoor((Doors)Random.Range(y * 2, (y + 1) * 2));
    }
    public Door GetDoor(Doors door)
    {
        switch (door)
        {
            case Doors.UL: return DoorUL;
            case Doors.UR: return DoorUR;
            case Doors.RU: return DoorRU;
            case Doors.RD: return DoorRD;
            case Doors.DL: return DoorDL;
            case Doors.DR: return DoorDR;
            case Doors.LU: return DoorLU;
            case Doors.LD: return DoorLD;
            default: return null;
        }
    }
}
