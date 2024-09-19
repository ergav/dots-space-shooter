using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate;
    public float spawnRateIncreaseInterval;
    public float spawnRateIncreaseValue;
    public float maxSpawnRate;

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Spawner
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                spawnPos = new float2(UnityEngine.Random.Range(-8f, 8f), 6),
            nextSpawnTime = authoring.spawnRate,
                spawnRate = authoring.spawnRate,
                spawnRateIncreaseInterval = authoring.spawnRateIncreaseInterval,
                spawnRateIncreaseValue = authoring.spawnRateIncreaseValue,
                maxSpawnRate = authoring.maxSpawnRate
            });
        }
    }
}