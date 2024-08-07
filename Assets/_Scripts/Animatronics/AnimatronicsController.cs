using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnimatronicsController : MonoBehaviour, IClockable, IAwakable, IUpdateable
{
    [Inject] private AnimatronicsDifficulty _animatronicsDifficulty;
    [SerializeField] private List<Animatronic> _animatronics;

    #region Interfaces
    public void OnAwake()
    {
        var difficulty = _animatronicsDifficulty.GetMemberById("test");
        foreach (var animatronic in _animatronics)
        {
            //Make difficulty based on night
            animatronic.SetDifficulty(difficulty.DifficultyLevel);

            Debug.Log($"{animatronic.gameObject.name} - awake");
        }
    }

    public void OnUpdate()
    {
        foreach (var ani in _animatronics)
            ani.OnUpdate();
    }

    public void Tick()
    {
        foreach (var animatromic in _animatronics)
        {
            ushort random = Convert.ToUInt16(UnityEngine.Random.Range(0, 125));
            animatromic.Turn(random);
        }
    }

    #endregion
}
