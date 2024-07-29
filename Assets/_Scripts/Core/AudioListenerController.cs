using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioListenerController
{
    public void LoccateTo(Vector2 position);
    public void Resset();
}
public class AudioListenerController : MonoBehaviour, IAudioListenerController
{
    public void LoccateTo(Vector2 position)
    {
        transform.position = position;
    }

    public void Resset()
    {
        transform.position = new Vector2(-1000,-1000);
    }
}
