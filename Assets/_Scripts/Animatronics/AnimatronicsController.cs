using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public interface IAnimatronicController
{
    
}
public class AnimatronicsController : MonoBehaviour, IClockable, IAwakable, IUpdateable
{
    [Inject] private AnimatronicsDifficulty _animatronicsDifficulty;
    [SerializeField] private List<Animatronic> _animatronics;

    public Sprite GetAnimatronicSprite(string id)
    {
        throw new NotImplementedException();
    }

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
            ushort random = Convert.ToUInt16(UnityEngine.Random.Range(0, 210));
            animatromic.Turn(random);
        }
    }

    #endregion
}
