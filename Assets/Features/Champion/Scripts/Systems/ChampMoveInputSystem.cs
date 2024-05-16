using Features.Champion.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using Unity.Physics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Champion.Scripts.Systems
{
    [UpdateInGroup(typeof(GhostInputSystemGroup))]
    public partial class ChampMoveInputSystem : SystemBase
    {
        private MobaActionMap inputActions;
        private CollisionFilter selectionFilter;

        protected override void OnCreate()
        {
            inputActions = new MobaActionMap();
            selectionFilter = new CollisionFilter()
            {
                BelongsTo = 1 << 5, // Raycasts
                CollidesWith = 1 << 0 //Ground Plane
            };
            RequireForUpdate<OwnerChampTag>();
        }

        protected override void OnStartRunning()
        {
            inputActions.Enable();
            inputActions.GameplayMap.SelectMovePosition.performed += OnSelectMovePosition;
        }

        protected override void OnStopRunning()
        {
            inputActions.GameplayMap.SelectMovePosition.performed -= OnSelectMovePosition;
            inputActions.Disable();
        }

        private void OnSelectMovePosition(InputAction.CallbackContext obj)
        {
            var collisionWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().CollisionWorld;
            var cameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
            var mainCamera = EntityManager.GetComponentObject<MainCamera>(cameraEntity).Value;

            var mousePos = Input.mousePosition;
            mousePos.z = 100f;
            var worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            var selectInput = new RaycastInput()
            {
                Start = mainCamera.transform.position,
                End = worldPos,
                Filter = selectionFilter
            };

            if (collisionWorld.CastRay(selectInput, out var closestHit))
            {
                var chempEntity = SystemAPI.GetSingletonEntity<OwnerChampTag>();
                EntityManager.SetComponentData(chempEntity, new ChempionMoveTargetPosition
                {
                    Value = closestHit.Position
                });
            }
        }

        protected override void OnUpdate()
        {
        }
    }
}