using MonoServices.Core;
using MonoServices.ObjectsSO;
using UnityEngine;

namespace MonoServices.Holders
{

    public class TexturesHolder : MonoService
    {
        [SerializeField] bool _getTexturesOnStart;
        [SerializeField] TexturesSO _texturesSO;

        protected override void Start()
        {
            base.Start();

            if (_getTexturesOnStart)
                GetTexturesCommand();
        }


        void GetTexturesCommand()
        {
            GetTextureArrayLengthCommand();
            InvokeCommand(0, _texturesSO);
        }

        void GetTextureWithIndexCommand(int textureIndex)
        {
            InvokeCommand(1, _texturesSO.Objs[textureIndex]);
        }

        void GetIndexTextureCommand(Texture2D texture)
        {
            for (int i = 0; i < _texturesSO.Objs.Length; i++)
            {
                if (_texturesSO.Objs[i] == texture)
                {
                    InvokeCommand(2);
                    break;
                }
            }
        }

        void GetTextureArrayLengthCommand() =>
            InvokeCommand(3, _texturesSO.Objs.Length);

        void SetTexturesCommand(TexturesSO textures)
        {
            _texturesSO = textures;
            InvokeCommand(4);

            GetTexturesCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetTexturesCommand();
            if (methodNumb == 1) GetTextureWithIndexCommand((int)passedObj);
            if (methodNumb == 2) GetIndexTextureCommand((Texture2D)passedObj);
            if (methodNumb == 4) SetTexturesCommand((TexturesSO)passedObj);
        }

    }
}