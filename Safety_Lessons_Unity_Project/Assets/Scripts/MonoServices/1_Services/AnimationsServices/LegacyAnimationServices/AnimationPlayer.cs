using MonoServices.Core;
using System.Linq;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimationPlayer : AnimationService
    {
        int _playIndex;

        string[] _animeNames;

        public int PlayIndex { get => _playIndex; set => _playIndex = value; }
        public string[] AnimeNames { get => _animeNames; set => _animeNames = value; }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (!_ThisAnimation)
                TryGetComponent(out _ThisAnimation);

            if (_ThisAnimation)
                _animeNames = _ThisAnimation.OfType<AnimationState>().Select(state => state.name).ToArray();
        }

        void PlayAnimationCommand()
        {
            if (_ThisAnimation)
            {
                if (_animeNames != null && _animeNames.Length > 0)
                {
                    var playAnimation = _animeNames.ElementAtOrDefault(_playIndex);
                    Debug.Log(playAnimation);

                    if (!string.IsNullOrWhiteSpace(playAnimation))
                    {
                        var isPlay = _ThisAnimation.isPlaying;
                        if (isPlay)
                            _ThisAnimation.Stop();

                        _ThisAnimation.Play(playAnimation);
                    }
                    else
                        Debug.Log("There doesn't exist such an animation clip!");

                    InvokeCommand(0);
                }
            }


        }

        void PauseAnimationCommand()
        {
            if (_ThisAnimation)
            {
                if (_animeNames != null && _animeNames.Length > 0)
                {
                    var playAnimation = _animeNames.ElementAtOrDefault(_playIndex);
                    Debug.Log(playAnimation);

                    if (!string.IsNullOrWhiteSpace(playAnimation))
                    {
                        var playState = _ThisAnimation[playAnimation];
                        if (playState)
                        {
                            var playable = playState.enabled;
                            if (playable)
                            {
                                playState.enabled = false;
                                InvokeCommand(1);
                            }
                        }
                    }
                    else
                        Debug.Log("There doesn't exist such an animation clip!");
                }
            }
        }

        void ResumeAnimationCommand()
        {
            if (_ThisAnimation)
            {
                if (_animeNames != null && _animeNames.Length > 0)
                {
                    var playAnimation = _animeNames.ElementAtOrDefault(_playIndex);

                    if (!string.IsNullOrWhiteSpace(playAnimation))
                    {
                        var playState = _ThisAnimation[playAnimation];
                        if (playState)
                        {
                            var playable = playState.enabled;
                            if (!playable)
                            {
                                playState.enabled = true;
                                InvokeCommand(2);
                            }
                        }
                    }
                    else
                        Debug.Log("There doesn't exist such an animation clip!");
                }
            }
        }

        void StopAnimationCommand()
        {
            if (_ThisAnimation)
            {
                if (_animeNames != null && _animeNames.Length > 0)
                {
                    var playAnimation = _animeNames.ElementAtOrDefault(_playIndex);

                    if (!string.IsNullOrWhiteSpace(playAnimation))
                    {
                        var played = _ThisAnimation.IsPlaying(playAnimation);
                        if (played)
                        {
                            _ThisAnimation.Stop(playAnimation);
                            InvokeCommand(3);
                        }
                    }
                    else
                        Debug.Log("There doesn't exist such an animation clip!");
                }
            }
        }

        void StopAllAnimationsCommand()
        {
            if (_ThisAnimation)
            {
                var isPlay = _ThisAnimation.isPlaying;
                if (isPlay)
                {
                    _ThisAnimation.Stop();
                    InvokeCommand(4);
                }
            }
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            switch (methodNumb)
            {
                case 0:
                    PlayAnimationCommand();
                    break;

                case 1:
                    PauseAnimationCommand();
                    break;

                case 2:
                    ResumeAnimationCommand();
                    break;

                case 3:
                    StopAnimationCommand();
                    break;

                case 4:
                    StopAllAnimationsCommand();
                    break;
            }
        }
    }

}
