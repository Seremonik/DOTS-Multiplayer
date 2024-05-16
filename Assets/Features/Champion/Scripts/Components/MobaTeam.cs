using Features.ServerConnection.Scripts;
using Unity.Entities;
using Unity.NetCode;

namespace Features.Champion.Scripts
{
    public struct MobaTeam : IComponentData
    {
        [GhostField]
        public TeamType Team;
    }
}