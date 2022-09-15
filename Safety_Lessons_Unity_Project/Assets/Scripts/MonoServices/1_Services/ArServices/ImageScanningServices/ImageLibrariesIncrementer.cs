using MonoServices.Core;
using UnityEngine;

namespace MonoServices.AR
{
    [RequireComponent(typeof(ARImageLibrariesGetter))]
    public class ImageLibrariesIncrementer : MonoService
    {

        int _currImageLibrary;

        void MoveToNextLibraryCommand()
        {

            _currImageLibrary++;
            InvokeCommand(0, _currImageLibrary);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            MoveToNextLibraryCommand();
    }
}
