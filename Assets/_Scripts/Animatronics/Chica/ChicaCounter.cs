using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

public class ChicaCounter : MonoBehaviour
{
    [SerializeField] private Color _checkedColor, _baseColor;

    [SerializeField] private float _totalDur;
    [SerializeField] private string _id;
    [SerializeField] private string _ressetSoundId;

    private ChicaCounterElement[] _elements;
    private ushort _limit;
    private ushort _counter = 0;
    private IPlayer _player;
    private AudioPlayer.Pool _audio;
    private IActionPointsManager _actionPointsManager;
  

    private void Start()
    {
        _elements = GetComponentsInChildren<ChicaCounterElement>();
        foreach (var c in _elements)
        {
            c.SetColor(_baseColor);
        }
        _limit = Convert.ToUInt16(_elements.Length);
    }

    [Inject]
    public void Inject(IActionPointsManager actionPointsManager, IPlayer player, AudioPlayer.Pool audio, IVentButton ventButton)
    {
        _player = player;
        actionPointsManager.AddOnGasEventListener(IncCounter);
        _actionPointsManager = actionPointsManager;
        _audio = audio;

        ventButton.AddOnClick(() => StartCoroutine(Resset()));
    }

    private IEnumerator Resset()
    {
        _actionPointsManager.DisableAction(ActionEnum.gas,_totalDur);
        _counter = 0;
        foreach (var c in _elements)
        {
            yield return new WaitForSeconds(_totalDur / _limit);
            c.SetColor(_baseColor);
            _audio.PlayAudio(_ressetSoundId,100,0);
        }
    }
    void IncCounter()
    {
        if(_counter < _limit)
            _elements[_counter].SetColor(_checkedColor);

        _counter++;
        if(_counter > _limit)
        {
            _player.Death(_id);
        }
    }

}
