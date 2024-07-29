using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Sprite/Sprite")]
public class RoomSprite : ScriptableObject, IStoreable
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _id;

    public Sprite Sprite { get => _sprite;}
    public string Id { get => _id; }
}
