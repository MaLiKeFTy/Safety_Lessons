using UnityEngine;

namespace MonoServices.SystemParameters
{
    public class DeviceOrentationHandler : MonoBehaviour
    {
        [SerializeField] ScreenOrientation screenOrientation = ScreenOrientation.LandscapeLeft;

        void Awake() =>
           ChangeSreenOrentation();

        void ChangeSreenOrentation() =>
            Screen.orientation = screenOrientation;
    }
}