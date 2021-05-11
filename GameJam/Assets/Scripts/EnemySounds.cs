using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] AudioSource enemyAudioSource = null;

    [SerializeField] AudioClip[] spawnSounds;
    [SerializeField] AudioClip[] attackSounds;
    [SerializeField] AudioClip[] deathSounds;

    void Start()
    {
        if (enemyAudioSource == null)
            enemyAudioSource = GetComponent<AudioSource>();

        //au cas où l'audio source est en play on awake au start
        enemyAudioSource.Stop();
        enemyAudioSource.clip = null;
        enemyAudioSource.playOnAwake = false;
    }

    public void PlaySpawnSound()
    {
        enemyAudioSource.clip = spawnSounds[Random.Range(0, spawnSounds.Length)];
        enemyAudioSource.Play();
    }

    public void PlayAttackSound()
    {
        enemyAudioSource.clip = attackSounds[Random.Range(0, attackSounds.Length)];
        enemyAudioSource.Play();
    }

    public void PlayDeathSound()
    {
        enemyAudioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
        enemyAudioSource.Play();
    }
}
