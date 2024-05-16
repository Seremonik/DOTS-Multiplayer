using Features.Combat.Components;
using Unity.Entities;
using UnityEngine;

namespace Features.Combat.Authoring
{
    public class AbilityAuthoring : MonoBehaviour
    {
        public GameObject AoeAbility;
        
        public class AbilityBaker : Baker<AbilityAuthoring>
        {
            public override void Bake(AbilityAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new AbilityPrefabs{AoeAbility = GetEntity(authoring.AoeAbility, TransformUsageFlags.Dynamic)});
            }
        }
    }
}