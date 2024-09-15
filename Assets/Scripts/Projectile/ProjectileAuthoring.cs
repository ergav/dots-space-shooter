using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float projectileSpeed;

    public class ProjectileAuthoringBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ProjectileMoveSpeed { value = authoring.projectileSpeed });
        }
    }
}