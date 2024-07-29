using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnimatronicsController : MonoBehaviour, IClockable, IAwakable
{
    [Inject] private AnimatronicsDifficulty _animatronicsDifficulty;
    [SerializeField] private List<Animatronic> _animatronics;

    #region Interfaces
    public void OnAwake()
    {
        foreach (var animatronic in _animatronics)
        {
            Debug.Log(animatronic.Id);
            var difficulty = _animatronicsDifficulty.GetMemberById(animatronic.Id);
            animatronic.SetParameters(difficulty);

            Debug.Log($"{animatronic.gameObject.name} - awake");
        }
    }
    public void Tick()
    {
        int random = Random.Range(0, 120);
        foreach (var animatromic in _animatronics)
            animatromic.Turn(random);
    }

    #endregion
}
