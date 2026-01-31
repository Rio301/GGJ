using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManejer : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicSource;

    [Header("SFX")]
    public AudioSource sfxSource;

    public AudioClip click, hit, miss, healtReduce1, maskSpawn1, maskSpawn2,healtReduce2, sunnyBg, rainBg;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = sunnyBg;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
