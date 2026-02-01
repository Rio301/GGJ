using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Scroring : MonoBehaviour
{
    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int scoreToRain;
    public int scoreToWin;

    [Header("Background")]
    public SpriteRenderer backgroundRain;
    public float transitionDuration = 2f;

    [Header("Lighting")]
    public Light2D GlobalLight;
    public float GlobalSunIntensity = 1f;
    public float GlobalRainIntensity = 0.1f;
    public List<Light2D> holeLight;
    public Light2D HammerLight;


    [Header("Spawner")]
    public Spawner spawner;
    public float friendlySpawnRate = 4f;
    public float maskedSpawnRate = 1f;
    public float EasterEggSpawnRate = 0.2f;

    [Header("Other")]
    public GameObject particle;

    private int lastState = 0; // prevents repeat transitions

    void Start()
    {
        Data.isRaining = false;
        spawner.spawnObjects[0].chance = 1f;
        spawner.spawnObjects[1].chance = 0f;
        spawner.spawnObjects[2].chance = 0f;
    }

    void Update()
    {
        if (Data.score >= scoreToWin)
            SceneManager.LoadScene("EndGame");
        int score = Data.score;
        scoreText.text = score.ToString();

        if (score >= scoreToRain && lastState != 1)
        {
            lastState = 1;
            Data.isRaining = true;
            particle.SetActive(true);
            StartCoroutine(Raining(0f, 1f, GlobalSunIntensity, GlobalRainIntensity));
            spawner.spawnObjects[0].chance = friendlySpawnRate;
            spawner.spawnObjects[1].chance = maskedSpawnRate;
            spawner.spawnObjects[2].chance = EasterEggSpawnRate;


        }
        else if (score < scoreToRain && lastState != 0)
        {
            lastState = 0;
            Data.isRaining = false;
            particle.SetActive(false);
            StartCoroutine(Raining(1f, 0f, GlobalSunIntensity, GlobalRainIntensity));
            spawner.spawnObjects[0].chance = 1f;
            spawner.spawnObjects[1].chance = 0f;
            spawner.spawnObjects[2].chance = 0f;
        }
    }

    IEnumerator Raining(float alphaFrom, float alphaTo, float intensityFrom, float intensityTo)
    {
        float t = 0f;
        Color c = backgroundRain.color;
        while (t < 1f)
        {
            t += Time.deltaTime / transitionDuration;
            c.a = Mathf.Lerp(alphaFrom, alphaTo, t);
            GlobalLight.intensity = Mathf.Lerp(intensityFrom, intensityTo, t);
            backgroundRain.color = c;
            foreach (var light in holeLight)
            {
                light.intensity = Mathf.Lerp(0f, 0.8f, t);
            }
            HammerLight.intensity = Mathf.Lerp(0f, 0.7f, t);
            yield return null;
        }
    }
}
