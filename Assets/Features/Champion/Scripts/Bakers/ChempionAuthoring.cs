using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

namespace Features.Champion.Scripts.Bakers
{
    public class ChempionAuthoring : MonoBehaviour
    {
        public class ChempionBaker : Baker<ChempionAuthoring>
        {
            public override void Bake(ChempionAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<ChempionTag>(entity);
                AddComponent<NewChempionTag>(entity);
                AddComponent<MobaTeam>(entity);
                AddComponent<URPMaterialPropertyBaseColor>(entity);
                AddComponent<ChempionMoveTargetPosition>(entity);
            }
        }
    }
}