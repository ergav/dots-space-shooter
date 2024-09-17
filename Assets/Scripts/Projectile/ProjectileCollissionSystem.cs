using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Physics;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct ProjectileCollissionSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;
        NativeArray<Entity> allEntities = entityManager.GetAllEntities();

        PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

        foreach (Entity entity in allEntities)
        {
            if (entityManager.HasComponent<ProjectileMoveSpeed>(entity)) 
            {
                LocalTransform bulletTransform = entityManager.GetComponentData<LocalTransform>(entity);
                entityManager.SetComponentData(entity, bulletTransform);

                NativeList<ColliderCastHit> hits = new NativeList<ColliderCastHit>(Allocator.Temp);
                physicsWorld.SphereCastAll(bulletTransform.Position, 0.5f, float3.zero, 1, ref hits,
                    new CollisionFilter { BelongsTo = (uint)CollisionLayer.Default, CollidesWith = (uint)CollisionLayer.Enemy });

                foreach (ColliderCastHit hit in hits)
                {
                    //entityManager.SetEnabled(hit.Entity, false);
                    //entityManager.SetEnabled(entity, false);
                    entityManager.DestroyEntity(hit.Entity);
                    entityManager.DestroyEntity(entity);
                }

                hits.Dispose();
            }          
        }
    }
}

public enum CollisionLayer
{
    Default = 1 << 0,
    Enemy = 1 << 6
}