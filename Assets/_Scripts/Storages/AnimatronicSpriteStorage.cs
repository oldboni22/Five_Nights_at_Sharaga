using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animatronics/SpriteStorage")]
public class AnimatronicSpriteStorage : Storage<AnimatronicSprite>
{
    [SerializeField] private AnimatronicSprite[] _sprites;
    protected override AnimatronicSprite[] Members => _sprites;
}
