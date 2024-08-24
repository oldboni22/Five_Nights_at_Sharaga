using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCamera : MonoBehaviour
{
    [SerializeField] private Transform[] _bounds;
    [SerializeField] private Camera _camera;
    private void Awake()
    {
        var bounds = new Bounds();
        foreach (Transform t in _bounds)
            bounds.Encapsulate(t.GetComponent<BoxCollider2D>().bounds);

        bounds.Expand(-.875f);
        var vert = bounds.size.y;
        var horizontal = bounds.size.x * Screen.height / Screen.width;

        _camera.orthographicSize = Mathf.Max(horizontal, vert) * .5f;

    }
}
