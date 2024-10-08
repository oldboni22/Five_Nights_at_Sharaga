using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animatronics/Difficulty")]
public class AnimatronicDifficulty : ScriptableObject, IStoreable
{
    [SerializeField] string _id;
    public string Id => _id;

    [SerializeField] private ushort _difficultyLevel;
    public ushort DifficultyLevel => _difficultyLevel;
}
