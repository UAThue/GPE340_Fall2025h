using UnityEngine;

public class ActionPlaySound : MonoBehaviour
{
    public void Start()
    {
    }

    public void PlaySound( AudioClip soundToPlay )
    {
        AudioSource.PlayClipAtPoint(soundToPlay, transform.position, 1.0f);
    }
}
