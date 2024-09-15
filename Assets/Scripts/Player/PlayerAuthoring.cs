using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float movementSpeed = 5;
    public GameObject projectilePrefab;

    class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerTag>(playerEntity);
            AddComponent<PlayerMoveInput>(playerEntity);

            AddComponent(playerEntity, new PlayerMoveSpeed
            {
                value = authoring.movementSpeed,
            });

            AddComponent<FireProjectileTag>(playerEntity);
            SetComponentEnabled<FireProjectileTag>(playerEntity, false);

            AddComponent(playerEntity, new ProjectilePrefab
            {
                value = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct PlayerMoveInput : IComponentData
{
    public float2 value;
}

public struct PlayerMoveSpeed : IComponentData
{
    public float value;
}

public struct PlayerTag : IComponentData { }

public struct ProjectilePrefab : IComponentData 
{
    public Entity value;
}

public struct ProjectileMoveSpeed : IComponentData
{
    public float value;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent { }