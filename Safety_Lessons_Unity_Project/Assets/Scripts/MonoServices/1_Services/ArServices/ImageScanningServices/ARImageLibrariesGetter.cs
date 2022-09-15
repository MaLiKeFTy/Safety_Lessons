using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace MonoServices.AR
{
    public class ARImageLibrariesGetter : MonoService
    {
        [SerializeField] List<XRReferenceImageLibrary> _imageLibraries;

        XRReferenceImageLibrary _currImageLibrary;

        protected override void Start()
        {
            base.Start();
            GetImageLibraryCommand(0);
        }

        void GetImageLibraryCommand(int libraryIndex)
        {
            _currImageLibrary = _imageLibraries[libraryIndex];

            InvokeCommand(0, _currImageLibrary);
            GetImageLibraryCountCommand(_currImageLibrary.count);
            GetImageLibraryNameCommand(_currImageLibrary.name);
        }

        void GetImageLibraryCountCommand(int count) =>
            InvokeCommand(1, count);

        void GetImageLibraryNameCommand(string name) =>
            InvokeCommand(2, name);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            GetImageLibraryTextureCommand((int)passedObj);

        void GetImageLibraryTextureCommand(int imageLibraryIndex) =>
            InvokeCommand(3, _currImageLibrary[imageLibraryIndex].texture);
    }
}
