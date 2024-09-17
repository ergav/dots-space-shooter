using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct  Spawner : IComponentData
{
    public Entity prefab;
    public float2 spawnPos;
    public float nextSpawnTime;
    public float spawnRate;
}