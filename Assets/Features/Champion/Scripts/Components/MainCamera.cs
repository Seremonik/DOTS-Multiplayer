using Unity.Entities;
using UnityEngine;

namespace Features.Champion.Scripts.Components
{
    public class MainCamera : IComponentData
    {
        public Camera Value;
    }

    public class MainCameraAuthoring : MonoBehaviour
    {
        public class MainCameraBaker : Baker<MainCameraAuthoring>
        {
            public override void Bake(MainCameraAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponentObject(entity, new MainCamera());
                AddComponent<MainCameraTag>(entity);
            }
        }
    }

    public struct MainCameraTag : IComponentData{}
}