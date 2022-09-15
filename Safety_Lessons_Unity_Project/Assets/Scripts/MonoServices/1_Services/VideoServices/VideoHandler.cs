using MonoServices.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace MonoSevices.Videos
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoHandler : MonoService
    {
        [SerializeField] VideoSequence _videoSequence;
        [SerializeField, Range(0, 1)] float videoPlayerVolume = 1;

        VideoPlayer _thisVideoPlayer;
        bool _videoFinishedPlaying;

        protected override void Awake()
        {
            base.Awake();

            _thisVideoPlayer = GetComponent<VideoPlayer>();


            _thisVideoPlayer.loopPointReached += (video) => _videoFinishedPlaying = true;

            ChangeVideoVolume();
        }

        void PlayVideoCommand()
        {
            ActivateCoroutine(PlayingVideosSequences());
        }

        void PlayVideFromSourceCommand()
        {
            _thisVideoPlayer.Play();
        }

        void LoopVideoCommand()
        {
            _thisVideoPlayer.isLooping = true;
        }

        void UnLoopVideoCommand()
        {
            _thisVideoPlayer.isLooping = false;
        }


        void ChangeVideoSquenceCommand(VideoSequence videoSequence)
        {
            _videoSequence = videoSequence;
        }


        void ChangeAndPlayVideoSquenceCommand(VideoSequence videoSequence)
        {
            if (videoSequence == null)
            {
                StopVideoCommand();
                return;
            }

            _videoSequence = videoSequence;
            PlayVideoCommand();
        }

        void StopVideoCommand()
        {
            _thisVideoPlayer.Stop();
        }


        void ChangeVideoVolume()
        {
            _thisVideoPlayer.SetDirectAudioVolume(0, videoPlayerVolume);
        }

        IEnumerator PlayingVideosSequences()
        {
            foreach (var audioClipHolder in _videoSequence.VideoPlayerHolders)
            {
                _videoFinishedPlaying = false;
                _thisVideoPlayer.clip = audioClipHolder.VideoClip;
                _thisVideoPlayer.isLooping = audioClipHolder.Loop;


                _thisVideoPlayer.Play();
                yield return new WaitWhile(() => !_videoFinishedPlaying);
            }


            yield return null;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) PlayVideoCommand();
            if (methodNumb == 1) PlayVideFromSourceCommand();
            if (methodNumb == 2) LoopVideoCommand();
            if (methodNumb == 3) UnLoopVideoCommand();
            if (methodNumb == 4) ChangeVideoSquenceCommand((VideoSequence)passedObj);
            if (methodNumb == 5) ChangeAndPlayVideoSquenceCommand((VideoSequence)passedObj);
        }
    }
}