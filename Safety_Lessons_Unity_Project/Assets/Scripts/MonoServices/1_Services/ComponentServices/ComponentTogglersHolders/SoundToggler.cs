using UnityEngine;

namespace MonoServices.Components
{
    public class SoundToggler : ComponentToggler
    {
        [SerializeField] AudioSource[] _audioSources;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _audioSources = comp.GetComponentsInChildren<AudioSource>();

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _audioSources)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _audioSources)
                child.enabled = true;
        }
    }
}