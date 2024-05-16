using Features.Champion.Scripts;
using Features.ServerConnection.Scripts;
using Unity.Entities;
using UnityEngine;

namespace Features.Team.Authoring
{
    public class MobaTeamAuthoring : MonoBehaviour
    {
        public TeamType MobaTeam;

        public class MobaTeamBaker : Baker<MobaTeamAuthoring>
        {
            public override void Bake(MobaTeamAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MobaTeam(){Team = authoring.MobaTeam});
            }
        }
    }
}