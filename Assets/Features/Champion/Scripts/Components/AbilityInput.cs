using Unity.Entities;
using Unity.NetCode;

namespace Features.Champion.Scripts.Components
{
    [GhostComponent(PrefabType = GhostPrefabType.AllPredicted)]
    public struct AbilityInput : IComponentData
    {
        [GhostField]public InputEvent AoeAbility;
    }
}