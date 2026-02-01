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
    public AudioSource soundManejer;
    int difficulty = GameManager.Instance.difficultyLevel;


    [System.Serializable]
    public class SpawnChance
    {
        public GameObject prefab;
        [Range(0, 100)] public float chance;
        public AudioClip spawnSFX;
    }
    void Start()
    {
        availablePoints = new List<Transform>(spawnPoints);
        StartCoroutine(SpawnRoutine());

        if (difficulty == 0)
        {
            spawnIntervalMin = 0.5f;
            spawnIntervalMax = 2f;
            lifeTimeMin = 2f;
            lifeTimeMax = 3f;
        }
        else if (difficulty == 1)
        {
            spawnIntervalMin = 0.5f;
            spawnIntervalMax = 2f;
            lifeTimeMin = 1f;
            lifeTimeMax = 1.5f;
        }
        else if (difficulty == 2)
        {
            spawnIntervalMin = 0f;
            spawnIntervalMax = 1.5f;
            lifeTimeMin = 0.6f;
            lifeTimeMax = 1.2f;
        }
        else if (difficulty == 3)
        {
            spawnIntervalMin = 0f;
            spawnIntervalMax = 1.5f;
            lifeTimeMin = 0.1f;
            lifeTimeMax = 0.9f;
        }

        IEnumerator SpawnRoutine()
        {
            while (true)
            {
                StartCoroutine(SpawnObjects());
                yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
            }
        }

        SpawnChance GetRandomSpawnData()
        {
            float totalChance = 0f;

            foreach (var item in spawnObjects)
                totalChance += item.chance;

            float randomValue = Random.Range(0, totalChance);

            foreach (var item in spawnObjects)
            {
                if (randomValue < item.chance)
                    return item;

                randomValue -= item.chance;
            }

            return spawnObjects[0];
        }


        IEnumerator SpawnObjects()
        {

            //List<Transform> availablePoints = new List<Transform>(spawnPoints);

            //for (int i = 0; i < spawnAmount; i++)
            //{
            //if (availablePoints.Count == 0)
            //    break;

            //if (active >= spawnAmount) yield return null;
            if (availablePoints.Count == 0) yield break;

            int index = Random.Range(0, availablePoints.Count);
            Transform randomPoint = availablePoints[index];

            SpawnChance data = GetRandomSpawnData();

            GameObject obj = Instantiate(
                data.prefab,
                randomPoint.position,
                Quaternion.identity
            );


            if (data.spawnSFX != null)
            {
                soundManejer.PlayOneShot(data.spawnSFX);
            }
            float lifeTime = Random.Range(lifeTimeMin, lifeTimeMax);
            Destroy(obj, lifeTime);


            availablePoints.RemoveAt(index);

            yield return new WaitForSeconds(lifeTime);
            availablePoints.Add(randomPoint);


            //}
        }
    }
}

