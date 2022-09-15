using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public sealed class TransformSiblingIndexHandler : TransformMonoService
    {
        [SerializeField] int transIndexToGet;
        [SerializeField] int _indexToSet;
        [SerializeField] bool _getParentLevelOnStart;

        Transform trans;


        protected override void Start()
        {
            base.Start();

            if (_getParentLevelOnStart)
                trans = TransformParentFinder.TranformParent(_ThisTransform, transIndexToGet);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetSiblingIndexCommand();
            if (methodNumb == 1) CheckSiblingIndexCommand((int)passedObj);
            if (methodNumb == 2) SetSiblingIndexCommand();
        }

        void GetSiblingIndexCommand()
        {
            trans = TransformParentFinder.TranformParent(_ThisTransform, transIndexToGet);
            InvokeCommand(0, trans.GetSiblingIndex());
        }

        void CheckSiblingIndexCommand(int indexToCheck)
        {
            var trans = TransformParentFinder.TranformParent(_ThisTransform, transIndexToGet);

            if (trans.GetSiblingIndex() == indexToCheck)
                InvokeCommand(1, indexToCheck);

        }

        void SetSiblingIndexCommand()
        {
            trans.SetSiblingIndex(_indexToSet);
        }
    }
}
