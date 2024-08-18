using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animatronics/Sprite")]
public class AnimatronicSprite : ScriptableObject, IStoreable
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _id;
    public string Id => _id;

    public Sprite Sprite { get => _sprite; }
}
