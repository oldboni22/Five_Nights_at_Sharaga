using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Kino;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        _room.OnCameraLeft();
        if (_camera.enabled)
            _audioListenerController.Resset();
    }
    public void EnableCamera()
    {
        _audioListenerController.LoccateTo(transform.position);

        _glitch.intensity = .1f;
        _enabled = true;
        _cameraButtonsController.SetColor(_id, CameraButtonColor.fine);
        _room.OnCameraOpened();

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
            _room.OnCameraLeft();

        _camera.enabled = false;
    }



    internal class RoomImageHandler
    {
        private readonly RoomSpriteArray _array;
        private readonly Image _image;

        private readonly List<string> _curAnimatronics = new List<string>();
        private string _curId;


        bool TryGetSprite(string id, out Sprite sprite)
        {
            sprite = null;
            if (_array.Exists(id))
                sprite = _array.GetMemberById(id).Sprite;

            return sprite != null;
        }

        internal void HandleAnimatronicEnter(string animatronicId)
        {
            _curAnimatronics.Add(animatronicId);

            Sprite sprite = null;
            if (TryGetSprite(animatronicId, out sprite))
            {
                _image.sprite = sprite;
                _curId = animatronicId;
            }
        }
        internal void HandleAnimatronicLeave(string animatronicId)
        {
            _curAnimatronics.Remove(animatronicId);

            if (animatronicId != _curId)
                return;

            if (_curAnimatronics.Count != 0)
            {
                Sprite sprite = null;
                foreach (var id in _curAnimatronics)
                {
                    if (TryGetSprite(id, out sprite))
                    {
                        _image.sprite = sprite;
                        _curId = id;
                        return;
                    }
                }
            }

            _image.sprite = _array.GetMemberById("0").Sprite;

        }

        internal RoomImageHandler(RoomSpriteArray array, Image image)
        {
            _array = array;
            _image = image;

            _image.sprite = _array.GetMemberById("0").Sprite;
        }
    }
}
