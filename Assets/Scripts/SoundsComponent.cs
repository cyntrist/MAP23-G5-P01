using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsComponent : MonoBehaviour
{
    public AudioClip _gameOver;
    public AudioClip _marioDies;
    public AudioClip _stageClear;
    public AudioClip _1Up;
    public AudioClip _brickSmash;
    public AudioClip _bump;
    public AudioClip _coin;
    public AudioClip _downTheFlagpole;
    public AudioClip _fireBall;
    public AudioClip _jump;
    public AudioClip _kick;
    public AudioClip _pause;
    public AudioClip _powerUp;
    public AudioClip _powerUpAppears;
    public AudioClip _stomp;

    [SerializeField]
    private static AudioSource _myAudioSource;


    static void PlaySound(AudioClip clip)
    {
        _myAudioSource.PlayOneShot(clip);
    }
}
