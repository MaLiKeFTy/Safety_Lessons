using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoUI
{
    public sealed class UiTextureComparer : UiMonoService
    {
        Texture2D _texture;
        bool _canExecute;

        void GetTextureCommand(Texture2D texture) =>
            _texture = texture;

        void CompareTextureCommand(Texture2D texture)
        {
            _canExecute = _texture = texture;

            CallEventCommand();
        }

        void CompareTextuesCommand(Texture2D[] textures)
        {
            _canExecute = false;

            foreach (var texture in textures)
            {
                if (_texture == texture)
                {
                    _canExecute = true;
                    break;
                }
            }

            CallEventCommand();
        }

        void CallEventCommand()
        {
            if (_canExecute)
                InvokeCommand(3);
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetTextureCommand((Texture2D)passedObj);
            if (methodNumb == 1) CompareTextureCommand((Texture2D)passedObj);
            if (methodNumb == 2) CompareTextuesCommand((Texture2D[])passedObj);
        }
    }
}
