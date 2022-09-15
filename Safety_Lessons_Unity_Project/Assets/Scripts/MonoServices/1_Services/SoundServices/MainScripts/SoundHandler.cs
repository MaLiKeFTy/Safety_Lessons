using MonoServices.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Sound
{
    public class SoundHandler : ParametersHandler
    {
        [SerializeField] List<SoundScriptableObject> soundSos = new List<SoundScriptableObject>();

        public List<string> EventNames => ParameterNames();

        public override bool isInvoker => true;

        protected override void Start()
        {
            base.Start();

            ActivateCoroutine(GetAudioClips());
        }

        IEnumerator GetAudioClips()
        {
            while (true)
            {
                for (int i = 0; i < EventNames.Count; i++)
                {
                    foreach (var soundSO in soundSos)
                        for (int j = 0; j < soundSO.soundGroups.Count; j++)
                            InvokeCommand(j, soundSO.soundGroups[j]);
                }

                yield return null;
            }
        }

        public override List<string> ParameterNames()
        {
            List<string> tempSoundNames = new List<string>();

            foreach (var item in soundSos)
            {
                foreach (var item1 in item.soundGroups)
                    if (!tempSoundNames.Contains(item1.GroupName))
                        tempSoundNames.Add(item1.GroupName);
            }

            return tempSoundNames;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}