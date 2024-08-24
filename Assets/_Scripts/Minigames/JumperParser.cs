using minigames.jumper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace minigames.jumper
{
    // Чтобы пофиксить краш, я юзнул по сути костыль
    // Если будущий я захочет это фиксануть, то нужно вынести все префабы сцены в скриптабл объект и уже из него брать все инстансы
    // Ну а пока что мне лень, пушто это не так критично
    //22.08.2024

    //23.08.2024 - ПЕРЕПИСАЛ ЭТО УЖАСНОЕ РЕШЕНИЕ НАХУЙ

    public class JumperParser : MonoBehaviour
    {
        [SerializeField] private MinigamePlayer _player;
        [SerializeField] private Exit _end;

        [Inject] private AudioPlayer.Pool _audio;
        [Inject] private JumperLayoutStorage _storage;

        [SerializeField] private FailTrig[] _trigs;

        private void Awake()
        {
            InstantiateLevel(_storage.GetMemberById("1"));
            Debug.Log("AWAKE");
        }
        void InstantiateLevel(jumperLayout layout)
        {
            Debug.Log("Parse");

            _player.SetSprite(layout.PlayerSprite);
            _player.transform.position = layout.StartPos;

            _end.transform.position = layout.EndPos;

            _audio.LoopAudio(layout.Ambience,.5f,255);

            GameObject.Instantiate(layout.Layout);

            foreach (var t in _trigs)
                t.SetId(layout.JumpscareId);
        }

    }
}