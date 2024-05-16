using Unity.Entities;
using Unity.NetCode;

namespace Features.Combat.Components
{
    [GhostComponent(PrefabType = GhostPrefabType.AllPredicted)]
    public struct DamageBufferElement : IBufferElementData
    {
        public int Value;
    }
}