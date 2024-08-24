using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sanity
{


    public class PlayerSanity
    {
        private readonly Player _player;
        private event Action<float> _onSanityChanged;

        private float _maxSanity = 100;
        private float _curSanity = 100;

        private float _nightmareProgress;

        internal IClock _clock;

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


                UpdateClock();
                _onSanityChanged.Invoke(_curSanity);
            }
        }


        void UpdateClock()
        {
            float sanityVar = 0;
            float GetMultiplyer(float sanity)
            {
                if (sanity >= 75)
                    return .075f;
                else if (sanity >= 50)
                    return .1f;
                else if (sanity >= 25)
                    return .125f;
                else return .15f;
            }

            if (_curSanity == 0)
            {

                sanityVar = 3f;
            }
            else
            {
                sanityVar = 1 - _curSanity / 100;

                sanityVar += ((Int16)(100 - _curSanity) / 10) * GetMultiplyer(_curSanity);
            }
            _clock.SetSanityVar(sanityVar);
        }

        public void AddOnChangedListener(Action<float> action) => _onSanityChanged += action;

    }
}