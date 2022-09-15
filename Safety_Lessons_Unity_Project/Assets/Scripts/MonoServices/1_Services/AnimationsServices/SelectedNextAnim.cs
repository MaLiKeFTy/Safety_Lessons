using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    public class SelectedNextAnim : MonoService
    {
        [SerializeField] AnimationClip _defaultClip;
        [SerializeField] bool _defaultNext;

        AnimationClip _selectedClip;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ClickedAndTimeCompletedCommand();
            if (methodNumb == 1) TimeCompletedCommand();
            if (methodNumb == 2) SetSelectedAnimationCommand((AnimationClip)passedObj);
            if (methodNumb == 5) GetRandomAnimationCommand((AnimationClip)passedObj);
        }

        void ClickedAndTimeCompletedCommand()
        {
            _defaultNext = false;
        }

        void TimeCompletedCommand()
        {
            if (!_defaultNext)
            {
                _selectedClip = _defaultClip;
                _defaultNext = true;
            }
            else
                RandomAnimationCommand();

            GetNextAnimationCommand();
        }

        void SetSelectedAnimationCommand(AnimationClip clip)
        {
            _selectedClip = clip;
            GetNextAnimationCommand();
        }

        void GetNextAnimationCommand() =>
            InvokeCommand(3, _selectedClip);

        void RandomAnimationCommand() =>
            InvokeCommand(4);

        void GetRandomAnimationCommand(AnimationClip clip)
        {
            _selectedClip = clip;
            _defaultNext = false;
        }
    }
}