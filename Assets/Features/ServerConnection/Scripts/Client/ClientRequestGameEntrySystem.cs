using Features.ServerConnection.Scripts.Common;
using Unity.Collections;
using Unity.Entities;
using Unity.NetCode;

namespace Features.ServerConnection.Scripts.Client
{
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation | WorldSystemFilterFlags.ThinClientSimulation)]
    public partial struct ClientRequestGameEntrySystem : ISystem
    {
        private EntityQuery pendingNetworkIdQuery;

        public void OnCreate(ref SystemState state)
        {
            var builder = new EntityQueryBuilder(Allocator.Temp).WithAll<NetworkId>().WithNone<NetworkStreamInGame>();
            pendingNetworkIdQuery = state.GetEntityQuery(builder);
            state.RequireForUpdate(pendingNetworkIdQuery);
            state.RequireForUpdate<ClientTeamRequest>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var requestedteam = SystemAPI.GetSingleton<ClientTeamRequest>().Value;
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            var pendingNetworkIds = pendingNetworkIdQuery.ToEntityArray(Allocator.Temp);

            foreach (var networkId in pendingNetworkIds)
            {
                ecb.AddComponent<NetworkStreamInGame>(networkId);
                var requestTeamEntity = ecb.CreateEntity();
                ecb.AddComponent(requestTeamEntity, new MobaTeamRequest()
                {
                    Value = requestedteam
                });
                ecb.AddComponent(requestTeamEntity, new SendRpcCommandRequest()
                {
                    TargetConnection = networkId
                });
            }
            ecb.Playback(state.EntityManager);
        }
    }
}