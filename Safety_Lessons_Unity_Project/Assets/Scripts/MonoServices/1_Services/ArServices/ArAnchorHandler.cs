using MonoServices.Core;
using UnityEngine.XR.ARFoundation;

namespace MonoServices.AR
{
    public class ArAnchorHandler : MonoService
    {
        void AddArAnchorCommand()
        {
            if (gameObject.GetComponent<ARAnchor>())
                return;

            gameObject.AddComponent<ARAnchor>();
        }

        void RemoveArAnchorCommand()
        {
            if (gameObject.GetComponent<ARAnchor>())
                Destroy(gameObject.GetComponent<ARAnchor>());
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) AddArAnchorCommand();
            if (methodNumb == 1) RemoveArAnchorCommand();
        }
    }
}
