using UnityEngine;
using UnityEngine.Video;

namespace MonoServices.Components
{
    public class VideoToggler : ComponentToggler
    {
        [SerializeField] VideoPlayer[] _videos;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _videos = comp.GetComponentsInChildren<VideoPlayer>();

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _videos)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _videos)
                child.enabled = true;
        }
    }
}
