using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour

{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn;
    public Transform[] spawnPoints;

    [Header("Timing Settings")]
    public float spawnInterval = 3f;
    public float lifeTime = 5f;

    [Header("Amount Settings")]
    public int spawnAmount = 1;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObjects();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject obj = Instantiate(objectToSpawn, randomPoint.position, Quaternion.identity);

            Destroy(obj, lifeTime);
        }
    }
}

