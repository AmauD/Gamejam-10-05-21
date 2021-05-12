using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSounds : MonoBehaviour
{
    [SerializeField] AudioSource gunAudioSource = null;

    [SerializeField] AudioClip[] fireSounds;
    [SerializeField] AudioClip[] reloadSounds;

    void Start()
    {
        if (gunAudioSource == null)
            gunAudioSource = GetComponent<AudioSource>();

        //au cas où l'audio source est en play on awake au start
        gunAudioSource.Stop();
        gunAudioSource.clip = null;
        gunAudioSource.playOnAwake = false;
    }

    public void PlayFireSound()
    {
        gunAudioSource.clip = fireSounds[Random.Range(0, fireSounds.Length)];
        gunAudioSource.Play();
    }

    public void PlayReloadSound()
    {
        gunAudioSource.clip = reloadSounds[Random.Range(0, reloadSounds.Length)];
        gunAudioSource.Play();
    }
}
