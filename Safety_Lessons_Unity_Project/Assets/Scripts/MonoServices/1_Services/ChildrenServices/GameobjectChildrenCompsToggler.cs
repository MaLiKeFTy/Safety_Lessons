using MonoServices.Core;
using MonoServices.Spawnning;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.Children
{
    public sealed class GameobjectChildrenCompsToggler : MonoService
    {
        [SerializeField] bool _startDisabled;
        [SerializeField] bool _disableMeshes = true;

        readonly List<MeshFilterHolder> _meshFilterHolders = new List<MeshFilterHolder>();
        readonly List<ParticleSystem> _turnedOffParticleSystems = new List<ParticleSystem>();

        protected override void Start()
        {
            base.Start();

            if (_startDisabled)
                DisableChildrenComponentsCommand();
        }

        [ContextMenu("Enable Children Components")]
        void EnableChildrenComponentsCommand()
        {
            ToggleChildrenComponents(true);
            InvokeCommand(0);
        }

        [ContextMenu("Disable Children Components")]
        void DisableChildrenComponentsCommand()
        {
            ToggleChildrenComponents(false);
            InvokeCommand(1);
        }


        void ToggleChildrenComponents(bool toggle)
        {
            Collider[] childrenColliders = GetComponentsInChildren<Collider>();
            foreach (var child in childrenColliders)
                child.enabled = toggle;

            DisableMeshes(toggle);

            SpawnningMonoService[] childrenSpawner = GetComponentsInChildren<SpawnningMonoService>();
            foreach (var child in childrenSpawner)
                child.enabled = toggle;

            Canvas[] childrenCanvases = GetComponentsInChildren<Canvas>();
            foreach (var child in childrenCanvases)
                child.enabled = toggle;

            ParticleSystem[] childrenParticleSystem = GetComponentsInChildren<ParticleSystem>();
            foreach (var child in childrenParticleSystem)
            {

                if (toggle)
                {
                    foreach (var turnedOffParticleSystem in _turnedOffParticleSystems)
                    {
                        if (turnedOffParticleSystem == child)
                            child.Play();
                    }
                }
                else
                {
                    if (child.isPlaying && !_turnedOffParticleSystems.Contains(child))
                        _turnedOffParticleSystems.Add(child);

                    child.Stop();
                }
            }

            Image[] childrenImages = GetComponentsInChildren<Image>();
            foreach(var child in childrenImages)
                child.enabled = toggle;            
            
            Text[] childrenText = GetComponentsInChildren<Text>();
            foreach(var child in childrenText)
                child.enabled = toggle;

            
        }

        void DisableMeshes(bool toggle)
        {
            if (!_disableMeshes)
                return;

            MeshFilter[] childrenMeshFilter = GetComponentsInChildren<MeshFilter>();


            if (!toggle)
                _meshFilterHolders.Clear();

            foreach (var child in childrenMeshFilter)
            {
                if (!toggle)
                {
                    if (child.mesh)
                    {
                        _meshFilterHolders.Add(new MeshFilterHolder
                        {
                            Obj = child.gameObject,
                            Mesh = child.mesh
                        });
                    }


                    child.mesh = null;
                }
                else
                {
                    foreach (var meshFilterHolder in _meshFilterHolders)
                    {
                        if (child.gameObject == meshFilterHolder.Obj)
                            child.mesh = meshFilterHolder.Mesh;
                    }
                }
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) EnableChildrenComponentsCommand();
            if (methodNumb == 1) DisableChildrenComponentsCommand();
        }

    }

    public class MeshFilterHolder
    {
        public GameObject Obj { get; set; }
        public Mesh Mesh { get; set; }
    }
}