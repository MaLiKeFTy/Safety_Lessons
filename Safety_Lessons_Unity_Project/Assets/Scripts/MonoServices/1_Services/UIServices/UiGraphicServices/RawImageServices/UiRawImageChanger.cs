using MonoServices.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.MonoUI
{
    [RequireComponent(typeof(RawImage))]
    public sealed class UiRawImageChanger : UiMonoService
    {
        [SerializeField] Texture2D _textureToChange;

        RawImage _rawImage;

        protected override void Awake()
        {
            base.Awake();

            _rawImage = GetComponent<RawImage>();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            Texture2D texture = passedObj is Texture2D ? (Texture2D)passedObj : _textureToChange;

            if (methodNumb == 0) GetRawImageTextureCommand(texture);
            if (methodNumb == 1) SetRawImageTextureCommand();
            if (methodNumb == 2) GetAndSetRawImageTextureCommand(texture);
        }

        void GetRawImageTextureCommand(Texture2D texture)
        {
            _textureToChange = texture;
        }

        void SetRawImageTextureCommand()
        {
            _rawImage.texture = _textureToChange;
        }

        void GetAndSetRawImageTextureCommand(Texture2D texture)
        {
            GetRawImageTextureCommand(texture);
            SetRawImageTextureCommand();
        }


    }
}
