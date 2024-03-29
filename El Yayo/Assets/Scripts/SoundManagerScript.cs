using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    [DoNotSerialize]public static SoundManagerScript instance;
    [Header("------------Audio Source --------------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    [Header("------------Audio Clips -------------")]
    public AudioClip background;
    public AudioClip item;
    public AudioClip attack;
    public AudioClip jump;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else Destroy(gameObject);
    }

    public void Start()
    {
    }
    public void StartMusic()
    {
        musicSource.volume = 0.35f;
        musicSource.clip = background;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public float ReturnVolume()
    {
        return musicSource.volume;
    }
    public float ReturnSFX()
    {
        return sfxSource.volume;
    }
    public void PlaySFX(AudioClip audio)
    {
        sfxSource.PlayOneShot(audio);
    }

    public void StopSound()
    {
        sfxSource.Stop();
    }
}
