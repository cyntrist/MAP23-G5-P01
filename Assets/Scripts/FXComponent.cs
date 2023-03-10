using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class FXComponent : MonoBehaviour
{
    public AudioClip _1Up;               //0
    public AudioClip _brickSmash;        //1
    public AudioClip _bump;              //2
    public AudioClip _coin;              //3
    public AudioClip _downTheFlagpole;   //4
    public AudioClip _fireBall;          //5
    public AudioClip _jump;              //6
    public AudioClip _kick;              //7
    public AudioClip _pause;             //8
    public AudioClip _powerUp;           //9
    public AudioClip _powerUpAppears;    //10
    public AudioClip _stomp;             //11

    public static AudioSource _myAudioSource;
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int clip)
    {
        switch (clip)
        {
            case 0:
                {
                    _myAudioSource.PlayOneShot(_1Up);
                    break;
                }
            case 1:
                {
                    _myAudioSource.PlayOneShot(_brickSmash);
                    break;
                }
            case 2:
                {
                    _myAudioSource.PlayOneShot(_bump);
                    break;
                }
            case 3:
                {
                    _myAudioSource.PlayOneShot(_coin);
                    break;
                }
            case 4:
                {
                    _myAudioSource.PlayOneShot(_downTheFlagpole);
                    break;
                }
            case 5:
                {
                    _myAudioSource.PlayOneShot(_fireBall);
                    break;
                }
            case 6:
                {
                    _myAudioSource.PlayOneShot(_jump);
                    break;
                }
            case 7:
                {
                    _myAudioSource.PlayOneShot(_kick);
                    break;
                }
            case 8:
                {
                    _myAudioSource.PlayOneShot(_pause);
                    break;
                }
            case 9:
                {
                    _myAudioSource.PlayOneShot(_powerUp);
                    break;
                }
            case 10:
                {
                    _myAudioSource.PlayOneShot(_powerUpAppears);
                    break;
                }
            case 11:
                {
                    _myAudioSource.PlayOneShot(_stomp);
                    break;
                }
        }
    }
}
