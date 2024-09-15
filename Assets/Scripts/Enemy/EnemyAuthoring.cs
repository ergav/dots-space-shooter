using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float moveSpeed;

    public class EnemyAuthoringBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            Entity enemyEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(enemyEntity, new EnemyMoveSpeed { value = authoring.moveSpeed });
            AddComponent<EnemyTag>(enemyEntity);  
            AddComponent(enemyEntity, new TargetPosition { value = float2.zero });
        }
    }
}

public struct EnemyMoveSpeed : IComponentData
{
    public float value;
}

public struct EnemyTag : IComponentData { }

public struct TargetPosition : IComponentData 
{
    public float2 value;
}