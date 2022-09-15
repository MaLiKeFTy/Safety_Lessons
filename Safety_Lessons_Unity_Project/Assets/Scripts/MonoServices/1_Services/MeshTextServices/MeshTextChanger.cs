using MonoServices.Core;
using TMPro;
using UnityEngine;

namespace MonoServices.MeshTexts
{
    [RequireComponent(typeof(TextMeshPro))]

    public class MeshTextChanger : MonoService
    {
        [SerializeField] string _textToChange;

        TextMeshPro _textPro;

        public string TextToChange { get => _textToChange; set => _textToChange = value; }

        protected override void Awake()
        {
            base.Awake();

            _textPro = GetComponent<TextMeshPro>();
        }

        void ChangeTextCommand()
        {
            _textPro.text = _textToChange;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            ChangeTextCommand();
        }
    }
}
