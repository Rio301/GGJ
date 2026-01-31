using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BoardLight : MonoBehaviour
{

    public List<Light2D> boardLights;
    public float offIntensity = 0.5f;
    public float onIntensity = 0.9f;

    void Awake()
    {
        boardLights = new List<Light2D>(
            GetComponentsInChildren<Light2D>()
        );
    }

    void Start()
    {
        StartCoroutine(Flashes());
    }

    IEnumerator Flashes()
    {
        foreach (Light2D light in boardLights)
        {
            StartCoroutine(IndividualFlash(light));
            yield return new WaitForSeconds(0.1f);
        }

        
    }

    IEnumerator IndividualFlash(Light2D light)
    {
        while (true)
        {
            light.intensity = offIntensity;
            yield return new WaitForSeconds(1f);

            light.intensity = onIntensity;
            yield return new WaitForSeconds(2f);
        }
    }
}
