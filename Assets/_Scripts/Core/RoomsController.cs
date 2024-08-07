using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsController : MonoBehaviour, IRoomsController
{
    [SerializeField] private readonly List<RoomHandler> _rooms = new List<RoomHandler>(14);
    [SerializeField] private float _cameraBreakTime;
    [SerializeField] private float _cameraRepairTime;

    public void AddRoom(RoomHandler room)
    {
        _rooms.Add(room);
        room.SetParams(new RoomParams
        {
            cameraBreakTime = _cameraBreakTime,
        });
    }
    public RoomHandler GetRandomRoom() => _rooms[Random.Range(1,_rooms.Count)];

    public string GetRandomRoomId() => _rooms[Random.Range(1, _rooms.Count)].Id;
    public RoomHandler GetRoomById(string id) => _rooms.Where(room => room.Id == id).Single();

}

public interface IRoomsController
{
    public RoomHandler GetRandomRoom();
    public string GetRandomRoomId();
    public RoomHandler GetRoomById(string id);
    public void AddRoom(RoomHandler room);
}