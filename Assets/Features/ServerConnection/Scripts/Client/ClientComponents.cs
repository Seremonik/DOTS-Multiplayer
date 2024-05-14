using Unity.Entities;

namespace Features.ServerConnection.Scripts.Client
{
    public struct ClientTeamRequest : IComponentData
    {
        public TeamType Value;
    }
}