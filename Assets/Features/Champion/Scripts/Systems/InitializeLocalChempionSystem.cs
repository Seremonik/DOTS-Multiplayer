using Features.Champion.Scripts.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine.Rendering;

namespace Features.Champion.Scripts.Systems
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation)]
    public partial struct InitializeLocalChempionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<NetworkId>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (transform, entity) in SystemAPI.Query<LocalTransform>().WithAll<GhostOwnerIsLocal>().WithNone<OwnerChampTag>().WithEntityAccess())
            {
                ecb.AddComponent<OwnerChampTag>(entity);
                ecb.SetComponent(entity, new ChempionMoveTargetPosition{ Value = transform.Position});
            }
            
            ecb.Playback(state.EntityManager);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}