using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimatorTransitionCompleted : AnimatorMonoService
    {
        [SerializeField] bool _checkForEmpty;
        [SerializeField] bool _checkForNonEmpty;
        [SerializeField] int _initialStateInfo;

        bool _inTransition;
        bool _isStateEmpty;
        bool _notEmpty;

        protected override void Start()
        {
            base.Start();
            ActivateCoroutine(TransitionCheck());
        }

        IEnumerator TransitionCheck()
        {
            while (true)
            {
                if (_ThisAnimator.IsInTransition(0))
                {
                    _initialStateInfo = _ThisAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
                    _inTransition = _ThisAnimator.IsInTransition(0);
                }

                if (_inTransition)
                {
                    var stateHash = _ThisAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;

                    if (_checkForEmpty && _initialStateInfo == stateHash)
                        _isStateEmpty = _ThisAnimator.GetCurrentAnimatorClipInfo(0).Length == 0;

                    if (_checkForNonEmpty && _initialStateInfo == stateHash)
                        _notEmpty = _ThisAnimator.GetCurrentAnimatorClipInfo(0).Length > 0;

                    if (!_ThisAnimator.IsInTransition(0))
                    {
                        if (_isStateEmpty)
                            TransitionFromEmptyStateCompletedCommand();
                        if (_notEmpty)
                            TransitionFromNonEmptyStateCompletedCommand();

                        TransitionCompletedCommand();

                        _inTransition = false;
                    }
                }

                yield return null;
            }
        }

        void TransitionCompletedCommand() =>
            InvokeCommand(0);

        void TransitionFromEmptyStateCompletedCommand() =>
            InvokeCommand(1);

        void TransitionFromNonEmptyStateCompletedCommand() =>
            InvokeCommand(2);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) { }
    }
}
