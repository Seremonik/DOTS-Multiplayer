using Features.ServerConnection.Scripts.Common;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;

namespace Features.ServerConnection.Scripts.Server
{
    [WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
    public partial struct ServerProcessGameEntryRequestSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var builder = new EntityQueryBuilder(Allocator.Temp).WithAll<MobaTeamRequest, ReceiveRpcCommandRequest>();
            state.RequireForUpdate<MobaPrefabs>();
            state.RequireForUpdate(state.GetEntityQuery(builder));
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var championPrefab = SystemAPI.GetSingleton<MobaPrefabs>().Champion;
            
            foreach (var (teamRequest, requestSource, requestEntity) in SystemAPI
                         .Query<MobaTeamRequest, ReceiveRpcCommandRequest>().WithEntityAccess())
            {
                ecb.DestroyEntity(requestEntity);
                ecb.AddComponent<NetworkStreamInGame>(requestSource.SourceConnection);

                var requestedTeamType = teamRequest.Value;

                if (requestedTeamType == TeamType.AutoAssign)
                {
                    requestedTeamType = TeamType.Blue;
                }

                var clientId = SystemAPI.GetComponent<NetworkId>(requestSource.SourceConnection).Value;

                Debug.Log("Server Id:" + clientId + " team:" + requestedTeamType);

                var newChamp = ecb.Instantiate(championPrefab);
                ecb.SetName(newChamp, "Champion");
                var newSpawnPosition = new float3(0, 1, 0);
                var newTransform = LocalTransform.FromPosition(newSpawnPosition);
                ecb.SetComponent(newChamp, newTransform);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}