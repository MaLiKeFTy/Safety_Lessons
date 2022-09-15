using MonoServices.Core;
using MonoServices.ObjectsSO;
using UnityEngine;

namespace MonoServices.Textures
{
    [RequireComponent(typeof(Renderer))]
    public class MatRandomTextureSetter : MonoService
    {
        [SerializeField] TexturesSO _texturesSO;
        [SerializeField] bool changeOnStart;
        [SerializeField] string textureParameter = "_BaseMap";

        Renderer _thisRenderer;

        protected override void Start()
        {
            base.Start();

            _thisRenderer = GetComponent<Renderer>();

            if (changeOnStart)
                SetRandomTextureCommand();
        }


        void SetRandomTextureCommand()
        {
            var randomIndex = Random.Range(0, _texturesSO.Objs.Length);

            _thisRenderer.material.SetTexture(textureParameter, _texturesSO.Objs[randomIndex]);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            SetRandomTextureCommand();
        }

    }
}