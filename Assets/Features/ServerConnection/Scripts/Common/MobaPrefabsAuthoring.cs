using Unity.Entities;
using UnityEngine;

namespace Features.ServerConnection.Scripts.Common
{
    public class MobaPrefabsAuthoring : MonoBehaviour
    {
        [SerializeField]
        private GameObject Champion;

        public class MobaPrefabBaker : Baker<MobaPrefabsAuthoring>
        {
            public override void Bake(MobaPrefabsAuthoring authoring)
            {
                var prefabContainerEntity = GetEntity(TransformUsageFlags.None);
                AddComponent(prefabContainerEntity, new MobaPrefabs()
                {
                    Champion = GetEntity(authoring.Champion, TransformUsageFlags.None)
                });
            }
        }
    }
}