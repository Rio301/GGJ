using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Visuals (Order matters)")]
    public List<SpriteRenderer> healthSprites; // left → right or bottom → top
    public List<Light2D> healthLights; // left → right or bottom → top

    [Header("Colors")]
    public Sprite fullColor;
    public Sprite emptyColor;

    [Header("Hurt")]
    public SpriteRenderer hurtEffect;
    public GameObject camGameObject;
    public Camera camCamera;
    public Light2D globalLight;
    SoundManejer soundManejer;

    private int maxHealth;
    private int currentHealth;

    void Start()
    {
        maxHealth = healthSprites.Count;
        currentHealth = healthSprites.Count;
        Data.health = maxHealth;

        soundManejer = GameObject
            .FindGameObjectWithTag("audio")
            .GetComponent<SoundManejer>();
    }

    void Update()
    {
        if (currentHealth != Data.health)
        {
            StartCoroutine(TriggerHurtEffect());
            StartCoroutine(TriggerThunderEffect());
            StartCoroutine(ScreenShake());
            soundManejer.sfxSource2.PlayOneShot(soundManejer.thunder);
        }


        currentHealth = Mathf.Clamp(Data.health, 0, maxHealth);

        for (int i = 0; i < healthSprites.Count; i++)
        {
            healthSprites[i].sprite = i < currentHealth ? fullColor : emptyColor;
            healthLights[i].enabled = i < currentHealth;
        }

        if (Data.health <= 0)
        {
            StartCoroutine(TriggerHurtEffect());
            StartCoroutine(TriggerThunderEffect());
            StartCoroutine(ScreenShake());
            soundManejer.sfxSource2.PlayOneShot(soundManejer.thunder);
            SceneManager.LoadScene("EndGame");
            //Debug.LogWarning("EndConditionnya Belum Di tambah");
        }
    }

    IEnumerator TriggerHurtEffect()
    {
        float t = 0f;
        Color c = hurtEffect.color;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.1f;
            c.a = Mathf.Lerp(0, 1, t);
            hurtEffect.color = c;
            yield return null;
        }
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 1f;
            c.a = Mathf.Lerp(1, 0, t);
            hurtEffect.color = c;
            yield return null;
        }
    }

    IEnumerator TriggerThunderEffect()
    {
        float temp = globalLight.intensity;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.03f;
            globalLight.intensity = Mathf.Lerp(temp, 0.1f, t);
            yield return null;
        }


        int numOfThunder = 3;
        for (int i=0; i < numOfThunder; i++)
        {
            yield return new WaitForSeconds(0.2f * i);

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / 0.03f;
                globalLight.intensity = Mathf.Lerp(0.1f, 0.5f, t);
                yield return null;
            }
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / 0.03f;
                globalLight.intensity = Mathf.Lerp(0.5f, 0.1f, t);
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.2f * numOfThunder);
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.03f;
            globalLight.intensity = Mathf.Lerp(0.1f, temp, t);
            yield return null;
        }
    }

    IEnumerator ScreenShake(
    float duration = 0.2f,
    float magnitude = 0.15f
)
    {
        Vector3 originalPos = camGameObject.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camGameObject.transform.localPosition =
                originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        camGameObject.transform.localPosition = originalPos;
    }

}
