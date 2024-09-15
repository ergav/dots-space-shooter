using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

public partial struct EnemyMoveSystem : ISystem
{
    private Entity player;
    private EntityManager entityManager;

    public void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        player = SystemAPI.GetSingletonEntity<PlayerTag>();

        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, EnemyMoveSpeed>())
        {
            float3 dir = entityManager.GetComponentData<LocalTransform>(player).Position - transform.ValueRO.Position;

            transform.ValueRW.Position += math.normalize(dir) * moveSpeed.value * deltaTime;
        }
    }
}