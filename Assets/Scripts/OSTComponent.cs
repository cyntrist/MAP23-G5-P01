using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OSTComponent : MonoBehaviour
{
    public AudioClip _runningAbout; //0
    public AudioClip _gameOver;     //1
    public AudioClip _marioDies;    //2
    public AudioClip _stageClear;   //3

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
                    _myAudioSource.Stop();
                    _myAudioSource.loop = true;
                    _myAudioSource.PlayOneShot(_runningAbout);
                    break;
                }
            case 1:
                {
                    _myAudioSource.loop = false;
                    _myAudioSource.PlayOneShot(_gameOver);
                    break;
                }
            case 2:
                {
                    _myAudioSource.loop = false;
                    _myAudioSource.PlayOneShot(_marioDies);
                    break;
                }
            case 3:
                {
                    _myAudioSource.Stop();
                    _myAudioSource.loop = false;
                    _myAudioSource.PlayOneShot(_stageClear);
                    break;
                }
        }
    }
}
