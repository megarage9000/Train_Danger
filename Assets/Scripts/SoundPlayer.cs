using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip[] sounds;

    AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayNoise(int index)
    {
        Utilities.playSound(source, sounds[index]);
    }
}
