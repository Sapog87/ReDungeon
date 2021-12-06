using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;
    public Door connectedDoor;
    public bool IsDoorOpened { get; set; }
    public bool IsDoorConnected { get; set; }
    public bool HasTunnel { get; set; }
    public Door ConnectedDoor { get => connectedDoor; set => connectedDoor = value; }
}
