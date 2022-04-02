using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public EightDoorsRoom[] eightDoorsRooms; //Массив комнат с 8 дверьми
    public FourDoorsRoom startingRoom; //Стартовая комната (4 двери)
    public FourDoorsRoom artifactRoom;
    public EightDoorsRoom bossRoom;
    public GameObject horizontalTunnel; //Ячейка горизонтального тунеля
    public GameObject verticalTunnel; //Ячейка вертикального тунеля
    private Room[,] spawnedRooms; //Сетка размещенных комнат
    private int n; //Размер сетки
    private int k; //Центр сетки
    private int countEightDoorsRooms; //Количество комнат с 8 дверьми

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player")
    }

    private void Start()
    {
        countEightDoorsRooms = eightDoorsRooms.Length;
        n = 9;
        k = n / 2;

        startingRoom.Position = FourDoorsRoom.Positions.RightDown; //Задает положение стартовой комнаты в ячейке сетке
        Room firstRoom = Instantiate(startingRoom);
        firstRoom.transform.position = new Vector2(0, 0);
        (firstRoom as FourDoorsRoom).OpenDoor((Room.Sides)Random.Range(0, 4)); //Открывает случайную дверь стартовой комнаты
        spawnedRooms = new Room[n, n];
        spawnedRooms[k, k] = firstRoom;

        while (countEightDoorsRooms-- > 0)
        {
            //yield return new WaitForSecondsRealtime(2f);
            if (!PlaceRoom(GetRandomRoom(countEightDoorsRooms)))
                break;
        }
        //PlaceRoom(artifactRoom);
        PlaceRoom(bossRoom);
        CloseDoors(); //Закрывает все двери которые остались открыты и не соединены 
        MakeTunnels(); //Строит тунели между соединенными комнатами
    }
    private bool PlaceRoom(Room room)
    {
        #region Форммирует сет мест, куда можно поставить комнату
        //Нужно изменить
        HashSet<(Vector2Int, List<FourDoorsRoom.Positions>, Room.Sides, Vector2Int)> vacantPlaces = new HashSet<(Vector2Int, List<FourDoorsRoom.Positions>, Room.Sides, Vector2Int)>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (HasOpenDoor(spawnedRooms[x, y], Room.Sides.Left) && x > 1 && spawnedRooms[x - 1, y] == null)
                    vacantPlaces.Add((new Vector2Int(x - 1, y), GetPossiblePositions(spawnedRooms[x, y], Room.Sides.Left), Room.Sides.Right, new Vector2Int(x, y)));
                if (HasOpenDoor(spawnedRooms[x, y], Room.Sides.Bottom) && y > 1 && spawnedRooms[x, y - 1] == null)
                    vacantPlaces.Add((new Vector2Int(x, y - 1), GetPossiblePositions(spawnedRooms[x, y], Room.Sides.Bottom), Room.Sides.Top, new Vector2Int(x, y)));
                if (HasOpenDoor(spawnedRooms[x, y], Room.Sides.Right) && x < maxX - 1 && spawnedRooms[x + 1, y] == null)
                    vacantPlaces.Add((new Vector2Int(x + 1, y), GetPossiblePositions(spawnedRooms[x, y], Room.Sides.Right), Room.Sides.Left, new Vector2Int(x, y)));
                if (HasOpenDoor(spawnedRooms[x, y], Room.Sides.Top) && y < maxY - 1 && spawnedRooms[x, y + 1] == null)
                    vacantPlaces.Add((new Vector2Int(x, y + 1), GetPossiblePositions(spawnedRooms[x, y], Room.Sides.Top), Room.Sides.Bottom, new Vector2Int(x, y)));
            }
        }
        #endregion

        if (vacantPlaces.Count == 0) return false;

        Room newRoom = Instantiate(room);

        //Выбирет случайное вакантное место
        Vector2Int position = new Vector2Int(); //Индекс ячейки, в которой находится комата 
        List<FourDoorsRoom.Positions> possiblePositions = new List<FourDoorsRoom.Positions>(); //Список позиций куда может стать новая комната
        Room.Sides side = Room.Sides.Top; //Сторона комнаты, с которой нужно открыть дверь
        Vector2Int parent = new Vector2Int(); //Индекс ячейки, в которой находится комата предок

        int limit = 500;
        while (limit-- > 0)
        {
            (position, possiblePositions, side, parent) = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (possiblePositions.Count != 0) break;
        }
        if (limit <= 0) return false;

        Door connectedDoorParent = null;
        Door connectedDoorChild = null;
        Vector2 offset = new Vector2(0, 0);

        if (newRoom is FourDoorsRoom)
        {
            (newRoom as FourDoorsRoom).Position = possiblePositions[Random.Range(0, possiblePositions.Count)];
            switch ((newRoom as FourDoorsRoom).Position)
            {
                case FourDoorsRoom.Positions.LeftUp:
                    offset = new Vector2(-10, 10);
                    break;
                case FourDoorsRoom.Positions.LeftDown:
                    offset = new Vector2(-10, 0);
                    break;
                case FourDoorsRoom.Positions.RightUp:
                    offset = new Vector2(0, 10);
                    break;
                case FourDoorsRoom.Positions.RightDown:
                    offset = new Vector2(0, 0);
                    break;
                default:
                    break;
            }
            connectedDoorChild = (newRoom as FourDoorsRoom).OpenDoor(side);
            (newRoom as FourDoorsRoom).OpenTwoDifferentDoors(side);
        }
        else
        {
            connectedDoorChild = (newRoom as EightDoorsRoom).OpenDoor(CorrectDoor(possiblePositions[0], side));
            (newRoom as EightDoorsRoom).OpenTwoDifferentDoors(side);
        }

        if (spawnedRooms[parent.x, parent.y] is EightDoorsRoom)
        {
            EightDoorsRoom.Doors connectedDoor = CorrectDoor(possiblePositions[0], Room.GetOppositeSide(side));
            connectedDoorParent = (spawnedRooms[parent.x, parent.y] as EightDoorsRoom).GetDoor(connectedDoor);
        }
        else
        {
            connectedDoorParent = (spawnedRooms[parent.x, parent.y] as FourDoorsRoom).GetDoor(Room.GetOppositeSide(side));
        }

        newRoom.transform.position = new Vector2(position.x - k, position.y - k) * 30 + offset;
        spawnedRooms[position.x, position.y] = newRoom;
        connectedDoorParent.ConnectedDoor = connectedDoorChild;
        connectedDoorChild.ConnectedDoor = connectedDoorParent;
        connectedDoorParent.IsDoorConnected = true;
        connectedDoorChild.IsDoorConnected = true;

        return true;
    }
    private Room GetRandomRoom(int i)
    { 
        return eightDoorsRooms[i];
    }
    private bool HasOpenDoor(Room room, Room.Sides side)
    {
        if (room is EightDoorsRoom)
        {
            switch (side)
            {
                case Room.Sides.Top:
                    if ((room as EightDoorsRoom).DoorUL.IsDoorOpened || (room as EightDoorsRoom).DoorUR.IsDoorOpened)
                        return true;
                    break;
                case Room.Sides.Right:
                    if ((room as EightDoorsRoom).DoorRU.IsDoorOpened || (room as EightDoorsRoom).DoorRD.IsDoorOpened)
                        return true;
                    break;
                case Room.Sides.Bottom:
                    if ((room as EightDoorsRoom).DoorDL.IsDoorOpened || (room as EightDoorsRoom).DoorDR.IsDoorOpened)
                        return true;
                    break;
                case Room.Sides.Left:
                    if ((room as EightDoorsRoom).DoorLU.IsDoorOpened || (room as EightDoorsRoom).DoorLD.IsDoorOpened)
                        return true;
                    break;
                default:
                    break;
            }
            return false;
        }
        else
        {
            switch (side)
            {
                case Room.Sides.Top:
                    if ((room as FourDoorsRoom).DoorU.IsDoorOpened)
                        return true;
                    break;
                case Room.Sides.Right:
                    if ((room as FourDoorsRoom).DoorR.IsDoorOpened)
                        return true;
                    break;
                case Room.Sides.Bottom:
                    if ((room as FourDoorsRoom).DoorD.IsDoorOpened)
                        return true;
                    break;
                case Room.Sides.Left:
                    if ((room as FourDoorsRoom).DoorL.IsDoorOpened)
                        return true;
                    break;
                default:
                    break;
            }
            return false;
        }
    }
    private EightDoorsRoom.Doors CorrectDoor(FourDoorsRoom.Positions position, Room.Sides side)
    {
        switch (side)
        {
            case Room.Sides.Top:
                switch (position)
                {
                    case FourDoorsRoom.Positions.LeftUp: return EightDoorsRoom.Doors.UL;
                    case FourDoorsRoom.Positions.LeftDown: return EightDoorsRoom.Doors.UL;
                    case FourDoorsRoom.Positions.RightUp: return EightDoorsRoom.Doors.UR;
                    case FourDoorsRoom.Positions.RightDown: return EightDoorsRoom.Doors.UR;
                    default: throw new System.Exception();
                }
            case Room.Sides.Right:
                switch (position)
                {
                    case FourDoorsRoom.Positions.LeftUp: return EightDoorsRoom.Doors.RU;
                    case FourDoorsRoom.Positions.LeftDown: return EightDoorsRoom.Doors.RD;
                    case FourDoorsRoom.Positions.RightUp: return EightDoorsRoom.Doors.RU;
                    case FourDoorsRoom.Positions.RightDown: return EightDoorsRoom.Doors.RD;
                    default: throw new System.Exception();
                }
            case Room.Sides.Bottom:
                switch (position)
                {
                    case FourDoorsRoom.Positions.LeftUp: return EightDoorsRoom.Doors.DL;
                    case FourDoorsRoom.Positions.LeftDown: return EightDoorsRoom.Doors.DL;
                    case FourDoorsRoom.Positions.RightUp: return EightDoorsRoom.Doors.DR;
                    case FourDoorsRoom.Positions.RightDown: return EightDoorsRoom.Doors.DR;
                    default: throw new System.Exception();
                }
            case Room.Sides.Left:
                switch (position)
                {
                    case FourDoorsRoom.Positions.LeftUp: return EightDoorsRoom.Doors.LU;
                    case FourDoorsRoom.Positions.LeftDown: return EightDoorsRoom.Doors.LD;
                    case FourDoorsRoom.Positions.RightUp: return EightDoorsRoom.Doors.LU;
                    case FourDoorsRoom.Positions.RightDown: return EightDoorsRoom.Doors.LD;
                    default: throw new System.Exception();
                }
            default: throw new System.Exception();
        }
    }
    private List<FourDoorsRoom.Positions> GetPossiblePositions(Room room, Room.Sides side)
    {
        List<FourDoorsRoom.Positions> positions = new List<FourDoorsRoom.Positions>();
        if (room is FourDoorsRoom)
        {
            FourDoorsRoom localRoom = room as FourDoorsRoom;
            switch (side)
            {
                case Room.Sides.Top:
                    if (localRoom.DoorU.IsDoorOpened)
                    {
                        positions.Add(localRoom.Position);
                        switch (localRoom.Position)
                        {
                            case FourDoorsRoom.Positions.LeftUp:
                                positions.Add(FourDoorsRoom.Positions.LeftDown);
                                break;
                            case FourDoorsRoom.Positions.LeftDown:
                                positions.Add(FourDoorsRoom.Positions.LeftUp);
                                break;
                            case FourDoorsRoom.Positions.RightUp:
                                positions.Add(FourDoorsRoom.Positions.RightDown);
                                break;
                            case FourDoorsRoom.Positions.RightDown:
                                positions.Add(FourDoorsRoom.Positions.RightUp);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case Room.Sides.Right:
                    if (localRoom.DoorR.IsDoorOpened)
                    {
                        positions.Add(localRoom.Position);
                        switch (localRoom.Position)
                        {
                            case FourDoorsRoom.Positions.LeftUp:
                                positions.Add(FourDoorsRoom.Positions.RightUp);
                                break;
                            case FourDoorsRoom.Positions.LeftDown:
                                positions.Add(FourDoorsRoom.Positions.RightDown);
                                break;
                            case FourDoorsRoom.Positions.RightUp:
                                positions.Add(FourDoorsRoom.Positions.LeftUp);
                                break;
                            case FourDoorsRoom.Positions.RightDown:
                                positions.Add(FourDoorsRoom.Positions.LeftDown);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case Room.Sides.Bottom:
                    if (localRoom.DoorD.IsDoorOpened)
                    {
                        positions.Add(localRoom.Position);
                        switch (localRoom.Position)
                        {
                            case FourDoorsRoom.Positions.LeftUp:
                                positions.Add(FourDoorsRoom.Positions.LeftDown);
                                break;
                            case FourDoorsRoom.Positions.LeftDown:
                                positions.Add(FourDoorsRoom.Positions.LeftUp);
                                break;
                            case FourDoorsRoom.Positions.RightUp:
                                positions.Add(FourDoorsRoom.Positions.RightDown);
                                break;
                            case FourDoorsRoom.Positions.RightDown:
                                positions.Add(FourDoorsRoom.Positions.RightUp);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case Room.Sides.Left:
                    if (localRoom.DoorL.IsDoorOpened)
                    {
                        positions.Add(localRoom.Position);
                        switch (localRoom.Position)
                        {
                            case FourDoorsRoom.Positions.LeftUp:
                                positions.Add(FourDoorsRoom.Positions.RightUp);
                                break;
                            case FourDoorsRoom.Positions.LeftDown:
                                positions.Add(FourDoorsRoom.Positions.RightDown);
                                break;
                            case FourDoorsRoom.Positions.RightUp:
                                positions.Add(FourDoorsRoom.Positions.LeftUp);
                                break;
                            case FourDoorsRoom.Positions.RightDown:
                                positions.Add(FourDoorsRoom.Positions.LeftDown);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            EightDoorsRoom localRoom = room as EightDoorsRoom;
            switch (side)
            {
                case Room.Sides.Top:
                    if (localRoom.DoorUL.IsDoorOpened)
                    {
                        positions.Add(FourDoorsRoom.Positions.LeftUp);
                        positions.Add(FourDoorsRoom.Positions.LeftDown);
                    }
                    else if (localRoom.DoorUR.IsDoorOpened)
                    {
                        positions.Add(FourDoorsRoom.Positions.RightUp);
                        positions.Add(FourDoorsRoom.Positions.RightDown);
                    }
                    break;
                case Room.Sides.Right:
                    if (localRoom.DoorRU.IsDoorOpened)
                    {
                        positions.Add(FourDoorsRoom.Positions.LeftUp);
                        positions.Add(FourDoorsRoom.Positions.RightUp);
                    }
                    else if (localRoom.DoorRD.IsDoorOpened)
                    {
                        positions.Add(FourDoorsRoom.Positions.LeftDown);
                        positions.Add(FourDoorsRoom.Positions.RightDown);
                    }
                    break;
                case Room.Sides.Bottom:
                    if (localRoom.DoorDL.IsDoorOpened)
                    {
                        positions.Add(FourDoorsRoom.Positions.LeftUp);
                        positions.Add(FourDoorsRoom.Positions.LeftDown);
                    }
                    else if (localRoom.DoorDR.IsDoorOpened)
                    {
                        positions.Add(FourDoorsRoom.Positions.RightUp);
                        positions.Add(FourDoorsRoom.Positions.RightDown);
                    }
                    break;
                case Room.Sides.Left:
                    if (localRoom.DoorLU.IsDoorOpened)
                    {
                        positions.Add(FourDoorsRoom.Positions.LeftUp);
                        positions.Add(FourDoorsRoom.Positions.RightUp);
                    }
                    else if (localRoom.DoorLD.IsDoorOpened)
                    {
                        positions.Add(FourDoorsRoom.Positions.LeftDown);
                        positions.Add(FourDoorsRoom.Positions.RightDown);
                    }
                    break;
                default:
                    break;
            }
        }
        return positions;
    }
    private void CloseDoors()
    {
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                if (spawnedRooms[x, y] is EightDoorsRoom)
                {
                    EightDoorsRoom localRoom = spawnedRooms[x, y] as EightDoorsRoom;
                    if (!localRoom.DoorUL.IsDoorConnected) localRoom.DoorUL.door.SetActive(true);
                    if (!localRoom.DoorUR.IsDoorConnected) localRoom.DoorUR.door.SetActive(true);
                    if (!localRoom.DoorRU.IsDoorConnected) localRoom.DoorRU.door.SetActive(true);
                    if (!localRoom.DoorRD.IsDoorConnected) localRoom.DoorRD.door.SetActive(true);
                    if (!localRoom.DoorDL.IsDoorConnected) localRoom.DoorDL.door.SetActive(true);
                    if (!localRoom.DoorDR.IsDoorConnected) localRoom.DoorDR.door.SetActive(true);
                    if (!localRoom.DoorLD.IsDoorConnected) localRoom.DoorLD.door.SetActive(true);
                    if (!localRoom.DoorLU.IsDoorConnected) localRoom.DoorLU.door.SetActive(true);
                }
                else
                {
                    FourDoorsRoom localRoom = spawnedRooms[x, y] as FourDoorsRoom;
                    if (!localRoom.DoorU.IsDoorConnected) localRoom.DoorU.door.SetActive(true);
                    if (!localRoom.DoorR.IsDoorConnected) localRoom.DoorR.door.SetActive(true);
                    if (!localRoom.DoorD.IsDoorConnected) localRoom.DoorD.door.SetActive(true);
                    if (!localRoom.DoorL.IsDoorConnected) localRoom.DoorL.door.SetActive(true);
                }

            }
        }
    }
    private void MakeTunnels()
    {
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                if (spawnedRooms[x, y] is EightDoorsRoom)
                {
                    EightDoorsRoom localRoom = spawnedRooms[x, y] as EightDoorsRoom;

                    if (localRoom.DoorUL.IsDoorConnected && !localRoom.DoorUL.HasTunnel)
                    {
                        CreateUpTunnel(localRoom.DoorUL);
                        localRoom.DoorUL.HasTunnel = true;
                        localRoom.DoorUL.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorUR.IsDoorConnected && !localRoom.DoorUR.HasTunnel)
                    {
                        CreateUpTunnel(localRoom.DoorUR);
                        localRoom.DoorUR.HasTunnel = true;
                        localRoom.DoorUR.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorDL.IsDoorConnected && !localRoom.DoorDL.HasTunnel)
                    {
                        CreateDownTunnel(localRoom.DoorDL);
                        localRoom.DoorDL.HasTunnel = true;
                        localRoom.DoorDL.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorDR.IsDoorConnected && !localRoom.DoorDR.HasTunnel)
                    {
                        CreateDownTunnel(localRoom.DoorDR);
                        localRoom.DoorDR.HasTunnel = true;
                        localRoom.DoorDR.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorRU.IsDoorConnected && !localRoom.DoorRU.HasTunnel)
                    {
                        CreateRightTunnel(localRoom.DoorRU);
                        localRoom.DoorRU.HasTunnel = true;
                        localRoom.DoorRU.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorRD.IsDoorConnected && !localRoom.DoorRD.HasTunnel)
                    {
                        CreateRightTunnel(localRoom.DoorRD);
                        localRoom.DoorRD.HasTunnel = true;
                        localRoom.DoorRD.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorLD.IsDoorConnected && !localRoom.DoorLD.HasTunnel)
                    {
                        CreateLeftTunnel(localRoom.DoorLD);
                        localRoom.DoorLD.HasTunnel = true;
                        localRoom.DoorLD.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorLU.IsDoorConnected && !localRoom.DoorLU.HasTunnel)
                    {
                        CreateLeftTunnel(localRoom.DoorLU);
                        localRoom.DoorLU.HasTunnel = true;
                        localRoom.DoorLU.ConnectedDoor.HasTunnel = true;
                    }

                }
                else
                {
                    FourDoorsRoom localRoom = spawnedRooms[x, y] as FourDoorsRoom;
                    if (localRoom.DoorU.IsDoorConnected && !localRoom.DoorU.HasTunnel)
                    {
                        CreateUpTunnel(localRoom.DoorU);
                        localRoom.DoorU.HasTunnel = true;
                        localRoom.DoorU.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorR.IsDoorConnected && !localRoom.DoorR.HasTunnel)
                    {
                        CreateRightTunnel(localRoom.DoorR);
                        localRoom.DoorR.HasTunnel = true;
                        localRoom.DoorR.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorD.IsDoorConnected && !localRoom.DoorD.HasTunnel)
                    {
                        CreateDownTunnel(localRoom.DoorD);
                        localRoom.DoorD.HasTunnel = true;
                        localRoom.DoorD.ConnectedDoor.HasTunnel = true;
                    }

                    if (localRoom.DoorL.IsDoorConnected && !localRoom.DoorL.HasTunnel)
                    {
                        CreateLeftTunnel(localRoom.DoorL);
                        localRoom.DoorL.HasTunnel = true;
                        localRoom.DoorL.ConnectedDoor.HasTunnel = true;
                    }
                }

            }
        }
    }
    private void CreateUpTunnel(Door door)
    {
        for (float i = door.transform.position.y + 1; i < door.ConnectedDoor.transform.position.y; i++)
        {
            GameObject Tunnel = Instantiate(verticalTunnel);
            Tunnel.transform.position = new Vector2(door.transform.position.x, i);
        }
    }
    private void CreateDownTunnel(Door door)
    {
        for (float i = door.transform.position.y; i > door.ConnectedDoor.transform.position.y + 1; i--)
        {
            GameObject Tunnel = Instantiate(verticalTunnel);
            Tunnel.transform.position = new Vector2(door.transform.position.x, i);
        }
    }
    private void CreateRightTunnel(Door door)
    {
        for (float i = door.transform.position.x + 1; i < door.ConnectedDoor.transform.position.x; i++)
        {
            GameObject Tunnel = Instantiate(horizontalTunnel);
            Tunnel.transform.position = new Vector2(i, door.transform.position.y);
        }
    }
    private void CreateLeftTunnel(Door door)
    {
        for (float i = door.transform.position.x; i > door.ConnectedDoor.transform.position.x + 1; i--)
        {
            GameObject Tunnel = Instantiate(horizontalTunnel);
            Tunnel.transform.position = new Vector2(i, door.transform.position.y);
        }
    }


    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = -1; i < 11; i++)
        {
            Gizmos.DrawLine(new Vector2(-140 + i * 30, -160), new Vector2(-140 + i * 30, 170));
        }
        for (int i = 0; i <= 11; i++)
        {
            Gizmos.DrawLine(new Vector2(-170, -160 + i * 30), new Vector2(160, -160 + i * 30));
        }
    }*/
}