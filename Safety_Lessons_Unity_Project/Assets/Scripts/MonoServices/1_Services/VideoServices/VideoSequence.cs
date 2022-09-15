using UnityEngine;

namespace MonoSevices.Videos
{
    [CreateAssetMenu(fileName = "VideoSO", menuName = "ScriptableObjects/ObjsSO/VideosSO")]
    public class VideoSequence : ScriptableObject
    {
        [SerializeField] VideoPlayerHolder[] _videoPlayerHolders = { new VideoPlayerHolder() };

        public VideoPlayerHolder[] VideoPlayerHolders => _videoPlayerHolders;
    }
}

