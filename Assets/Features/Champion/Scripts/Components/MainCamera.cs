using Unity.Entities;
using UnityEngine;

namespace Features.Champion.Scripts.Components
{
    public class MainCamera : IComponentData
    {
        public Camera Value;
    }

    public struct MainCameraTag : IComponentData{}
}