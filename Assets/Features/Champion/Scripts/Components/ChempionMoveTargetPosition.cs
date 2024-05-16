using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;

namespace Features.Champion.Scripts
{
    public struct ChempionTargetPosition : IInputComponentData
    {
        public float3 Value;
    }
}