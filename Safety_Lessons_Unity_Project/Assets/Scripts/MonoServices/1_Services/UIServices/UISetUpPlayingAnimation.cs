using MonoServices.Core;
using MonoServices.Holders;
using UnityEngine;

namespace MonoServices.MonoUI
{
    public sealed class UISetUpPlayingAnimation : UiMonoService
    {
        [SerializeField] GameObject _playingAnim;
        [SerializeField] AnimationHolder _animHolder;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                SetPlayingAnimCommand((AnimationClip)passedObj);
            else
                GetUITemplateCommand((GameObject)passedObj);
        }

        void SetPlayingAnimCommand(AnimationClip clip)
        {
            if (clip && _animHolder)
                _animHolder.HolderDetails(clip);
        }

        void GetPlayingAnimCommand(GameObject playingAnimHolder) =>
            InvokeCommand(1, playingAnimHolder);

        void GetUITemplateCommand(GameObject template)
        {
            _playingAnim = template;
            _animHolder = _playingAnim.GetComponent<AnimationHolder>();

            GetPlayingAnimCommand(_playingAnim);
        }
    }
}