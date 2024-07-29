using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Room/Sprite/Array")]
public class RoomSpriteArray : Storage<RoomSprite>, IStoreable
{
    [SerializeField] private string _roomId;
    [SerializeField] private RoomSprite[] _sprites;
    public string Id => _roomId;

    protected override RoomSprite[] Members => _sprites;
}
