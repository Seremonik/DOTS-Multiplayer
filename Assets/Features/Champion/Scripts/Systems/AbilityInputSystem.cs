using Features.Champion.Scripts.Components;
using Unity.Burst;
using Unity.Entities;

namespace Features.Champion.Scripts.Systems
{
    public partial class AbilityInputSystem : SystemBase
    {
        private MobaActionMap actionMap;

        protected override void OnCreate()
        {
            actionMap = new MobaActionMap();
        }

        protected override void OnStartRunning()
        {
            actionMap.Enable();
        }

        protected override void OnStopRunning()
        {
            actionMap.Disable();
        }

        protected override void OnUpdate()
        {
            var newAbilityInput = new AbilityInput();

            if (actionMap.GameplayMap.AoeAbility.WasPressedThisFrame())
            {
                newAbilityInput.AoeAbility.Set();
            }

            foreach (var abilityInput in SystemAPI.Query<RefRW<AbilityInput>>())
            {
                abilityInput.ValueRW = newAbilityInput;
            }
        }
    }
}