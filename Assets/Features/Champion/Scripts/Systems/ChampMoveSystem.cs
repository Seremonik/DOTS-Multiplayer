using Features.Champion.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
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
                moveTarget.y = transform.ValueRO.Position.y;

                if (math.distancesq(transform.ValueRO.Position, moveTarget) < 0.001f) 
                {
                    continue;
                }

                var moveDirection = math.normalize(moveTarget - transform.ValueRO.Position);
                var moveVector = moveDirection * moveSpeed.Value * deltaTime;
                transform.ValueRW.Position += moveVector;
                transform.ValueRW.Rotation = quaternion.LookRotationSafe(moveDirection, math.up());
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}