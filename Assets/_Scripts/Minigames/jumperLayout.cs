using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace minigames.jumper
{

    [CreateAssetMenu(menuName ="minigames/jumper/layout")]
    public class jumperLayout : ScriptableObject,IStoreable
    {
        [SerializeField] private Sprite _playerSprite;
        [SerializeField] private Sprite _bg;
        [SerializeField] private AudioClip _ambience;
        [SerializeField] private string _jumpscareId;

        [SerializeField] private string _id;

        [SerializeField] private GameObject _layout;
        [SerializeField] private Vector2 _startPos, _endPos;

        public string Id => _id;

        public Sprite PlayerSprite { get => _playerSprite;  }
        public Sprite Bg { get => _bg; }
        public AudioClip Ambience { get => _ambience; }
        public string JumpscareId { get => _jumpscareId; }
        public GameObject Layout { get => _layout;  }
        public Vector2 StartPos { get => _startPos; }
        public Vector2 EndPos { get => _endPos;  }
    }

    
}