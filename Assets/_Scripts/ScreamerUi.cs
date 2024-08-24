using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

public class ScreamerUi : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetSprite(Sprite sprite ) => _image.sprite = sprite;
}
