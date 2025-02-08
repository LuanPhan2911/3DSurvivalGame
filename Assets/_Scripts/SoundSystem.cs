using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem Instance { get; private set; }

    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] public AudioClipSO audioClipSO;

    [SerializeField] public PlayerAudioClipSO playerAudioClipSO;







    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        PlayBackgroundMusic(audioClipSO.startingBackgroundMusic);
    }
    public void PlayBackgroundMusic(AudioClip audioClip)
    {
        backgroundAudioSource.clip = audioClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.Play();
    }
    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume = 0.5f)
    {

    }




}
