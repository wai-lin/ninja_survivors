using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    public AudioSource pause;
    public AudioSource resume;
    public AudioSource gameOver;
    
    public AudioSource enemyDie;
    public AudioSource enemyGetHit;
    
    public AudioSource levelUpClick;
    
    public AudioSource weaponSpawn;
    public AudioSource weaponDespawn;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
    
    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.6f, 1.3f);
        sound.Stop();
        sound.Play();
    }
}
