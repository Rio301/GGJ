using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour

{
    public SpawnChance[] spawnObjects;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    private List<Transform> availablePoints;

    [Header("Timing Settings")]
    public float spawnIntervalMin = 0f;
    public float spawnIntervalMax = 2f;
    public float lifeTimeMin = 2f;
    public float lifeTimeMax = 3f;

    [Header("Amount Settings")]
    public int spawnAmount = 1;
    //public int active = 0;

    [System.Serializable]
    public class SpawnChance
    {
        public GameObject prefab;
        [Range(0, 100)] public float chance;
    }
    void Start()
    {
        availablePoints = new List<Transform>(spawnPoints);
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            StartCoroutine(SpawnObjects());
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
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

    IEnumerator SpawnObjects()
    {
        
        //List<Transform> availablePoints = new List<Transform>(spawnPoints);

        //for (int i = 0; i < spawnAmount; i++)
        //{
        //if (availablePoints.Count == 0)
        //    break;

        //if (active >= spawnAmount) yield return null;
        if(availablePoints.Count == 0) yield break;

        int index = Random.Range(0, availablePoints.Count);
        Transform randomPoint = availablePoints[index];

        GameObject prefabToSpawn = GetRandomPrefab();
        GameObject obj = Instantiate(prefabToSpawn, randomPoint.position, Quaternion.identity);
        float lifeTime = Random.Range(lifeTimeMin, lifeTimeMax);
        Destroy(obj, lifeTime);


        availablePoints.RemoveAt(index);

        yield return new WaitForSeconds(lifeTime);
        availablePoints.Add(randomPoint);


        //}
    }
}

