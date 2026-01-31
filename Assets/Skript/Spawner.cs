using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour

{
    public SpawnChance[] spawnObjects;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;

    [Header("Timing Settings")]
    public float spawnInterval = 3f;
    public float lifeTime = 5f;

    [Header("Amount Settings")]
    public int spawnAmount = 1;

    [System.Serializable]
    public class SpawnChance
    {
        public GameObject prefab;
        [Range(0, 100)] public float chance;
    }
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
    
    GameObject GetRandomPrefab()
    {
        float totalChance = 0f;

        foreach (var item in spawnObjects)
            totalChance += item.chance;

        float randomValue = Random.Range(0, totalChance);

        foreach (var item in spawnObjects)
        {
            if (randomValue < item.chance)
                return item.prefab;

            randomValue -= item.chance;
        }

        return spawnObjects[0].prefab;
    }

    void SpawnObjects()
    {
        
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < spawnAmount; i++)
        {
            if (availablePoints.Count == 0)
                break;

            int index = Random.Range(0, availablePoints.Count);
            Transform randomPoint = availablePoints[index];

            GameObject prefabToSpawn = GetRandomPrefab();
            GameObject obj = Instantiate(prefabToSpawn, randomPoint.position, Quaternion.identity);
            Destroy(obj, lifeTime);


            availablePoints.RemoveAt(index);
        }
    }
}

