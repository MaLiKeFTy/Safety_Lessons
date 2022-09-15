using UnityEngine;
using UnityEngine.Video;

namespace MonoSevices.Videos
{
    [System.Serializable]
    public class VideoPlayerHolder
    {
        [SerializeField] VideoClip _videoClip;
        [SerializeField] bool _loop;

        public VideoClip VideoClip => _videoClip;
        public bool Loop => _loop;
    }
}

