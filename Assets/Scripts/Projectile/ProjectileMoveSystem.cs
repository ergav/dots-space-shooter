using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine;

public partial struct ProjectileMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

        foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>,ProjectileMoveSpeed>())
        {
            transform.ValueRW.Position += transform.ValueRO.Up() * moveSpeed.value *  deltaTime;
        }
    }
}