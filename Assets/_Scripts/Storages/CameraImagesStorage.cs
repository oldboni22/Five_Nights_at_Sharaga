using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Images Storage", fileName = "Camera Images")]
public class CameraImagesStorage : Storage<RoomSpriteArray>
{
    [SerializeField] private RoomSpriteArray[] _arrays;
    protected override RoomSpriteArray[] Members => _arrays;

}
