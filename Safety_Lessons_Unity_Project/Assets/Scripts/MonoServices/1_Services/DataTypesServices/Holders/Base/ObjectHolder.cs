using MonoServices.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.Holders
{
    public abstract class ObjectHolder<T> : MonoService
    {
        [SerializeField] protected T _heldObject;

        protected Text _holderText;

        protected override void Awake()
        {
            base.Awake();

            _holderText = GetComponentInChildren<Text>();
        }


        protected virtual void UseHolderCommand() =>
            InvokeCommand(0, _heldObject);

        public void HolderDetails(T obj)
        {
            _heldObject = obj;
            SetHolderText();
        }

        protected virtual void SetHolderText() { }

        public T GetHolderDetails()
        {
            return _heldObject;
        }

        public string GetHolderText()
        {
            return _holderText.text;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            UseHolderCommand();
    }
}