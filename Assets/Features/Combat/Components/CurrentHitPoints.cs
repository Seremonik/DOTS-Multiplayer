using Unity.Entities;
using Unity.NetCode;

namespace Features.Combat.Components
{
    public struct CurrentHitPoints : IComponentData
    {
        [GhostField] public int Value;
    }
}