using Features.ServerConnection.Scripts;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;

namespace Features.Champion.Scripts.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial struct InitializeCharacterSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (mass, mobaTeam, entity) in SystemAPI.Query<RefRW<PhysicsMass>, MobaTeam>().WithAny<NewChempionTag>().WithEntityAccess())
            {
                mass.ValueRW.InverseInertia[0] = 0;
                mass.ValueRW.InverseInertia[1] = 0;
                mass.ValueRW.InverseInertia[2] = 0;

                var teamColor = mobaTeam.Team switch
                {
                    TeamType.Blue => new float4(0, 0, 1, 1),
                    TeamType.Red => new float4(1, 0, 0, 1),
                    _ => new float4(1)
                };
                
                ecb.SetComponent(entity, new URPMaterialPropertyBaseColor(){Value = teamColor});
                ecb.RemoveComponent<NewChempionTag>(entity);
            }
            ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}