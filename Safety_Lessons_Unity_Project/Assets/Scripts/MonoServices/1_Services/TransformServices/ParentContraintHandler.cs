using MonoServices.Core;
using UnityEngine;
using UnityEngine.Animations;

namespace MonoServices.Transforms
{
    [RequireComponent(typeof(ParentConstraint))]
    public class ParentContraintHandler : MonoService
    {
        ParentConstraint _parentContraint;

        protected override void Awake()
        {
            base.Awake();

            _parentContraint = GetComponent<ParentConstraint>();
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeForwardTranslationValueCommand((float)passedObj);
            if (methodNumb == 1) TurnOffContraintCommand();
        }

        void ChangeForwardTranslationValueCommand(float value)
        {
            _parentContraint.SetTranslationOffset(0, new Vector3(_parentContraint.GetTranslationOffset(0).x, _parentContraint.GetTranslationOffset(0).y, _parentContraint.GetTranslationOffset(0).z + value));
        }

        void TurnOffContraintCommand()
        {
            _parentContraint.constraintActive = false;
        }
    }
}
