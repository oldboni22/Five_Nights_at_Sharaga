using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity
{
    private readonly Player _player;
    private event Action<float> _onSanityChanged;

    private float _maxSanity = 100;
    private float _curSanity = 100;

    private float _nightmareProgress;

    public PlayerSanity(Player player)
    {
        _player = player;
    }

    public float CurSanity
    {
        get => _curSanity;
        set
        {
            _curSanity = value;

            if (_curSanity > _maxSanity)
            {
                _curSanity = _maxSanity;
            }
            else if (_curSanity < 0)
            {
                _curSanity = 0;
                _nightmareProgress += value;
                Debug.Log("nightmare " + _nightmareProgress);
                if (_nightmareProgress <= -30)
                {
                    
                    _player.Death("nightmare");
                }
                return;
            }

            _onSanityChanged.Invoke(_curSanity);
        }
    }

    public void AddOnChangedListener(Action<float> action) => _onSanityChanged += action;

}
