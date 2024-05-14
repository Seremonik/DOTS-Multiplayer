using Unity.NetCode;

namespace Features.ServerConnection.Scripts.Common
{
    public struct MobaTeamRequest : IRpcCommand
    {
        public TeamType Value;
    }
}