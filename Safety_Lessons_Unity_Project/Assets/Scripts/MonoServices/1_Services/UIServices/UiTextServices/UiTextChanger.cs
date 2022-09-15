using MonoServices.Core;
using TMPro;
using UnityEngine;

namespace MonoServices.MonoUI
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class UiTextChanger : UiMonoService
    {
        [SerializeField] string _textToChange;

        TextMeshProUGUI _textMesh;

        protected override void Awake()
        {
            base.Awake();

            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            var text = passedObj is string ? (string)passedObj : _textToChange;



            if (methodNumb == 0) GetTextCommand(text);
            if (methodNumb == 1) SetTextCommand();
            if (methodNumb == 2) GetAndSetTextCommand(text);
        }

        void GetTextCommand(string text) =>
            _textToChange = text;

        void SetTextCommand() =>
            _textMesh.text = _textToChange;

        void GetAndSetTextCommand(string text)
        {
            GetTextCommand(text);
            SetTextCommand();
        }

    }
}
