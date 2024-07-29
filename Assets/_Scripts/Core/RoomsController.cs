using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsController : MonoBehaviour, IRoomsController, IAwakable
{
    [SerializeField] private RoomHandler[] _rooms;
    [SerializeField] private float _cameraBreakTime;

    public RoomHandler GetRandomRoom() => _rooms[Random.Range(1,_rooms.Length)];

    public string GetRandomRoomId() => _rooms[Random.Range(1, _rooms.Length)].Id;
    public RoomHandler GetRoomById(string id) => _rooms.Where(room => room.Id == id).Single();

    public void OnAwake()
    {
        foreach(RoomHandler room in _rooms)
        {
            room.SetCameraBreakTime(_cameraBreakTime);
            room.OnAwake();
        }
            
    }
}

public interface IRoomsController
{
    public RoomHandler GetRandomRoom();
    public string GetRandomRoomId();
    public RoomHandler GetRoomById(string id);
}