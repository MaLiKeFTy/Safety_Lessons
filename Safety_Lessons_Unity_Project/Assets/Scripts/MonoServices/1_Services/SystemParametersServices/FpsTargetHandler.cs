using UnityEngine;

namespace MonoServices.SystemParameters
{
    public class FpsTargetHandler : MonoBehaviour
    {

        [SerializeField] int targetFps = 30;

        void OnEnable() =>
            Application.targetFrameRate = targetFps;
    }
}