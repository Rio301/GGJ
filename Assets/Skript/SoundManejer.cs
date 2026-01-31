using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManejer : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicSource;
    public AudioSource musicSource2;
    public float musicTransitionDuration = 4f;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioSource sfxSource2;
    public AudioSource sfxSource3;
    public AudioSource sfxSource4;

    public AudioClip click, hit, miss, healtReduce1, maskSpawn1, maskSpawn2,healtReduce2, sunnyBg, rainBg, thunder;

    private bool isRaining = false;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = sunnyBg;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.isRaining && !isRaining)
        {
            isRaining = true;
            StartCoroutine(PlayRainMusic());
        }
    }

    IEnumerator PlayRainMusic()
    {
        musicSource2.volume = 0f;
        musicSource2.Play();

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / musicTransitionDuration;
            musicSource.volume = Mathf.Lerp(1f, 0f, t);
            musicSource2.volume = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        musicSource.Stop();
    }
}
