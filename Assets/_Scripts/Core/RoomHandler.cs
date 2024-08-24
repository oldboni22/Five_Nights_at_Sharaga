using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Kino;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public struct RoomParams
{
    public float cameraBreakTime;
}
public class RoomHandler : MonoBehaviour, IUpdateable
{
    [SerializeField] private string _floorString;
    public string FloorString => _floorString;

    #region variables
    private IAudioListenerController _audioListenerController;
    private ICameraButtonsController _cameraButtonsController;
    private OverlayImage.Pool _overlayPool;

    [SerializeField] private DigitalGlitch _glitch;
    [SerializeField] private string _id;
    public string Id => _id;

    private Tween _tween;

    private float _cameraBreakTime;
    private float _curCameraBreak;

    private bool _enabled = true;

    private Room _room;
    private RoomImageHandler _imageHandler;

    [SerializeField] private Camera _camera;

    [SerializeField] Image _image;

    [Inject]
    public void Inject(IAudioListenerController audioListenerController, CameraImagesStorage cameraImages, ICameraButtonsController cameraButtonsController, OverlayImage.Pool overlayPool, IRoomsController roomsController)
    {
        _audioListenerController = audioListenerController;
        _imageHandler = new RoomImageHandler(cameraImages.GetMemberById(_id), _image);

        _cameraButtonsController = cameraButtonsController;
        _overlayPool = overlayPool;

        _room = new Room(_id);
        roomsController.AddRoom(this);
    }

   

    public void SetParams(RoomParams @params)
    {
        _cameraBreakTime = @params.cameraBreakTime;
    }

    #endregion



    #region DOTween
    private void Start()
    {
        if (_id == "main")
            return;

        var trans = _image.GetComponent<RectTransform>();
        trans.localScale = new Vector3(1.25f, 1, 1);
        trans.DOLocalMoveX(220, .1f);

        _tween = trans.DOLocalMoveX(-220, _cameraBreakTime / 4).SetLoops(-1, LoopType.Yoyo);
        _tween.Pause();
    }

    void EnableTween()
    {
        _tween?.Play();
    }
    void DisableTween()
    {
        _tween?.Pause();
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
    #endregion

    #region Interfaces

    public void OnUpdate()
    {
        HandleCameraBreak();
    }
    #endregion

    #region Camera-visuals
    void HandleCameraBreak()
    {
        if (_enabled is false)
            return;

        _curCameraBreak += Time.deltaTime;
        if (_curCameraBreak >= _cameraBreakTime)
        {
            DisableCamera();
            _curCameraBreak = 0;
        }
    }
    public void DisableCamera()
    {
        _glitch.intensity = 1;
        _enabled = false;
        _cameraButtonsController.SetColor(_id, CameraButtonColor.broken);


        if (_camera.enabled)
        {
            _room.OnCameraLeft();
            _audioListenerController.Resset();
            DisableTween();
        }
    }
    public void EnableCamera()
    {
        if (_camera.enabled)
        {
            _audioListenerController.LoccateTo(transform.position);
            _room.OnCameraOpened();
            EnableTween();
        }

        _glitch.intensity = .1f;
        _enabled = true;
        _cameraButtonsController.SetColor(_id, CameraButtonColor.fine);
        
        StartCoroutine(CameraOpenedGlitch(.15f));
    }
    IEnumerator CameraOpenedGlitch(float delay)
    {
        _glitch.intensity = 1;
        yield return new WaitForSeconds(delay);


        if (_enabled)
            _glitch.intensity = .1f;
    }


    public RoomHandler SetOverlay(Canvas canvas)
    {
        canvas.worldCamera = _camera;
        return this;
    }

    public void SetOverlayImage(SetOverlayImageParams @params)
    {
        _overlayPool.SetOverlay(@params);
    }
    public void RemoveOverlayImage(string id)
    {
        _overlayPool.RemoveById(id, this);
    }
    #endregion

    #region Animatronic-related
    public void AnimatronicLeave(Animatronic animatronic)
    {
        _room.AnimatronicLeave(animatronic);
        _imageHandler.HandleAnimatronicLeave(animatronic.Id);
        StartCoroutine(CameraOpenedGlitch(.22f));

        if (_camera.enabled)
            animatronic.OnCameraLeave();
    }
    public void AnimatronicEnter(Animatronic animatronic)
    {
        _room.AnimatronicEnter(animatronic);
        _imageHandler.HandleAnimatronicEnter(animatronic.Id);
        StartCoroutine(CameraOpenedGlitch(.22f));

        if (_camera.enabled)
            animatronic.OnCameraDetect();
    }
    #endregion

    public void Blackout(float dur)
    {
        if(_id != "main")
            StartCoroutine(BlackOut(dur));
    }

    IEnumerator BlackOut(float dur)
    {
        bool wasEnabled = _enabled;
        DisableCamera();
        
        yield return new WaitForSeconds(dur);

        if (wasEnabled)
            EnableCamera();

    }



    public void ConnectTo()
    {
        _camera.enabled = true;

        if (_enabled)
        {
            EnableTween();
            _room.OnCameraOpened();
            _audioListenerController.LoccateTo(transform.position);
        }

        else
            _audioListenerController.Resset();



        if (_glitch != null)
            StartCoroutine(CameraOpenedGlitch(.3f));

    }
    public void GasRoom()
    {
        _room.OnGas();
    }

    public void DisconnectFrom()
    {
        if (_enabled)
        {
            _room.OnCameraLeft();
            DisableTween();
        }

        _camera.enabled = false;
    }



    internal class RoomImageHandler
    {
        private readonly RoomSpriteArray _array;
        private readonly Image _image;

        private readonly List<RoomSprite> _curAnimatronics = new List<RoomSprite>();
        private RoomSprite _cur;


        bool TryGetSprite(string id, out RoomSprite sprite)
        {
            sprite = null;
            if (_array.Exists(id))
                sprite = _array.GetMemberById(id);

            return sprite != null;
        }

        void SetSprite(RoomSprite sprite)
        {
            _image.sprite = sprite.Sprite;
            _cur = sprite;
        }
        internal void HandleAnimatronicEnter(string animatronicId)
        {
            RoomSprite sprite = null;
            if (TryGetSprite(animatronicId, out sprite))
            {
                if (sprite.Prio > _cur.Prio)
                {
                    SetSprite(sprite);
                }
                _curAnimatronics.Add(sprite);
            }
        }
        internal void HandleAnimatronicLeave(string animatronicId)
        {

            if (_array.Exists(animatronicId) is false ^ animatronicId != _cur.Id)
                return;

            _curAnimatronics.Remove(_cur);

            if (_curAnimatronics.Count != 0)
            {
                var biggestPrio = _curAnimatronics.OrderByDescending(i => i.Prio).First();
                SetSprite(biggestPrio);
            }
            else
                SetSprite(_array.GetMemberById("0"));

        }

        internal RoomImageHandler(RoomSpriteArray array, Image image)
        {
            _array = array;
            _image = image;

            SetSprite(_array.GetMemberById("0"));
        }
    }
}
