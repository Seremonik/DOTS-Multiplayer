using Features.Champion.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Features.Champion.Scripts.Systems
{
    [WorldSystemFilter((WorldSystemFilterFlags.ClientSimulation))]
    public partial class InitializeMainCameraSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<MainCameraTag>();
        }

        protected override void OnUpdate()
        {
            Enabled = false;
            var mainCameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
            EntityManager.SetComponentData(mainCameraEntity, new MainCamera{Value = Camera.main});
            var z = 7;
        }
    }
}