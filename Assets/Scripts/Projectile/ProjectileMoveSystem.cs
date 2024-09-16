using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;

public partial struct ProjectileMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        EntityManager entityManager = state.EntityManager;
        NativeArray<Entity> allEntities = entityManager.GetAllEntities();

        PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

        foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>,ProjectileMoveSpeed>())
        {
            transform.ValueRW.Position += transform.ValueRO.Up() * moveSpeed.value *  deltaTime;

            NativeList<ColliderCastHit> hits = new NativeList<ColliderCastHit>(Allocator.Temp);
            physicsWorld.SphereCastAll(transform.ValueRO.Position, 0.5f, float3.zero, 1, ref hits, 
                new CollisionFilter { BelongsTo = (uint)CollisionLayer.Default, CollidesWith = (uint)CollisionLayer.Enemy });

            foreach (ColliderCastHit hit in hits) 
            { 
                entityManager.SetEnabled(hit.Entity, false); 
            }

            hits.Dispose();
        }
    }
}

public enum CollisionLayer
{
    Default = 1 << 0,
    Enemy = 1 << 0
}