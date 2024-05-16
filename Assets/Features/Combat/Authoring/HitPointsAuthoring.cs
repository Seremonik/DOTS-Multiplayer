using Features.Combat.Components;
using Unity.Entities;
using UnityEngine;

namespace Features.Combat.Authoring
{
    public class HitPointsAuthoring : MonoBehaviour
    {
        public int MaxHitPoints;
     
        public class HitPointBaker : Baker<HitPointsAuthoring>
        {
            public override void Bake(HitPointsAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CurrentHitPoints() {Value = authoring.MaxHitPoints});
                AddComponent(entity, new MaxHitPoints() {Value = authoring.MaxHitPoints});
                AddBuffer<DamageBufferElement>(entity);
                AddBuffer<DamageThisTick>(entity);
            }
        }
    }
}