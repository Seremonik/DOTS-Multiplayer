using Features.Champion.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;

namespace Features.Champion.Scripts.Systems
{
    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    public partial struct ChampMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, moveTargetPosition, moveSpeed) in SystemAPI
                         .Query<RefRW<LocalTransform>, ChempionMoveTargetPosition, CharacterMoveSpeed>()
                         .WithAll<Simulate>())
            {
                var moveTarget = moveTargetPosition.Value;
                moveTarget
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}