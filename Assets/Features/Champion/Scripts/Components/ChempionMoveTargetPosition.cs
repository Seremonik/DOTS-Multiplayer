using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;

namespace Features.Champion.Scripts
{
    [GhostComponent(PrefabType = GhostPrefabType.AllPredicted)]
    public struct ChempionMoveTargetPosition : IInputComponentData
    {
        [GhostField(Quantization = 0)]public float3 Value;
    }
}