using UnityEngine;

[CreateAssetMenu(menuName = "Animatronics/Difficulties", fileName = "AnimatronicDifficulties")]
public class AnimatronicsDifficulty : Storage<AnimatronicDifficulty>
{
    [SerializeField] AnimatronicDifficulty[] difficulties;

    protected override AnimatronicDifficulty[] Members => difficulties;
}
