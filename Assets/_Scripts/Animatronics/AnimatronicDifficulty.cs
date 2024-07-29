using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animatronics/Difficulty")]
public class AnimatronicDifficulty : ScriptableObject, IStoreable
{
    [SerializeField] string _id;
    public string Id => _id;

    [SerializeField] private int _failedTicksTreshold;
    [SerializeField] private int _difficultyLevel;

    public int FailedTicksTreshold => _failedTicksTreshold;
    public int DifficultyLevel => _difficultyLevel;
}
