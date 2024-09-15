using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        new PlayerMoveJob
        {
            deltaTime = deltaTime
        }.Schedule();
    }
}

[BurstCompile]
public partial struct PlayerMoveJob : IJobEntity
{
    public float deltaTime;

    [BurstCompile]
    private void Execute(ref LocalTransform transform, in PlayerMoveInput input, PlayerMoveSpeed speed)
    {
        transform.Position.xy += input.value * speed.value * deltaTime;
    }
}