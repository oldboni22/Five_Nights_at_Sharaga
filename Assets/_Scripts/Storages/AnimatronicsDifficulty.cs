using UnityEngine;

[CreateAssetMenu(menuName = "Animatronics/Difficulties", fileName = "AnimatronicDifficulties")]
public class AnimatronicsDifficulty : Storage<AnimatronicDifficulty>
{
    [SerializeField] AnimatronicDifficulty[] animatronics;

    protected override AnimatronicDifficulty[] Members => animatronics;
}
