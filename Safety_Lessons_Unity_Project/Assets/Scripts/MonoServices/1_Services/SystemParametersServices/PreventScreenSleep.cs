using UnityEngine;

namespace MonoServices.SystemParameters
{
    public class PreventScreenSleep : MonoBehaviour
    {
        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}