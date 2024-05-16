using Features.Champion.Scripts;
using Features.Champion.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Features.Combat.Components
{
    public readonly partial struct AoeAspect : IAspect
    {
        private readonly RefRO<AbilityInput> abilityInput;
        private readonly RefRO<AbilityPrefabs> abilityPrefabs;
        private readonly RefRO<MobaTeam> mobaTeam;
        private readonly RefRO<LocalTransform> localTransform;

        public bool ShouldAttack => abilityInput.ValueRO.AoeAbility.IsSet;
        public Entity AbilityPrefab => abilityPrefabs.ValueRO.AoeAbility;
        public MobaTeam Team => mobaTeam.ValueRO;
        public float3 attackPosition => localTransform.ValueRO.Position;
    }
}