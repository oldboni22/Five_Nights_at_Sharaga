using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public struct SetOverlayImageParams
{
    public Sprite sprite;
    public RoomHandler room;
    public int prio;
    public string id;
}
public class OverlayImage : MonoBehaviour
{
    internal string _id;
    private RoomHandler _curRoom;

    [SerializeField] internal Canvas _canvas;
    [SerializeField] private Image _image;
    internal Pool _pool;

    internal void SetOverlay(SetOverlayImageParams @params)
    {
        _curRoom = @params.room.SetOverlay(_canvas);

        _image.sprite = @params.sprite;
        _canvas.sortingOrder = @params.prio;

        _id = @params.id;
        gameObject.SetActive(true);
    }
    void RemoveOverlay()
    {
        _pool.Despawn(this);
    }
    internal void OnRemoveCallHandler(RoomHandler room, string id)
    {
        if (gameObject.activeInHierarchy is false)
            return;

        if (_curRoom == room && id == _id)
            RemoveOverlay();
    }
    public class Pool : MonoMemoryPool<OverlayImage>
    {
        internal event Action<RoomHandler, string> _onRemoveCall;
        protected override void OnCreated(OverlayImage item)
        {
            item._pool = this;
            _onRemoveCall += item.OnRemoveCallHandler;
        }
        protected override void OnDespawned(OverlayImage item)
        {
            item._canvas.worldCamera = null;
            item.gameObject.SetActive(false);
        }
        
        public void SetOverlay(SetOverlayImageParams @params)
        {
            Spawn().SetOverlay(@params);
        }
        public void RemoveById(string id, RoomHandler room)
        {
            _onRemoveCall.Invoke(room, id);
        }
    }
}
