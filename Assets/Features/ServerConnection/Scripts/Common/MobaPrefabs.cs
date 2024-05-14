using Unity.Entities;
using UnityEngine;

namespace Features.ServerConnection.Scripts.Common
{
    public struct MobaPrefabs : IComponentData
    {
        public Entity Champion;
    }
}