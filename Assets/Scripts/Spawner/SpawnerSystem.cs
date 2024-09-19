using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct SpawnerSystem : ISystem
{
    private float spawnTimer;

    public void OnUpdate(ref SystemState state) 
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
                float3 pos = new float3 (spawner.ValueRO.spawnPos.x, spawner.ValueRO.spawnPos.y, 0);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
                spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;

                spawner.ValueRW.spawnPos = new float2(UnityEngine.Random.Range(-8f, 8f), 6);       
            }

            if (spawner.ValueRO.spawnRate <= spawner.ValueRO.maxSpawnRate)
            {
                return;
            }

            spawnTimer += deltaTime;

            if (spawnTimer >= spawner.ValueRO.spawnRateIncreaseInterval)
            {
                spawner.ValueRW.spawnRate -= spawner.ValueRO.spawnRateIncreaseValue;
                if (spawner.ValueRW.spawnRate < spawner.ValueRW.maxSpawnRate)
                {
                    spawner.ValueRW.spawnRate = spawner.ValueRO.maxSpawnRate;
                }
                spawnTimer = 0;
                Debug.Log("Timer Increase");
            }
        }
    }
}