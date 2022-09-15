using MonoServices.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Sound
{
    public class SoundsSequenceHolder : MonoService
    {
        [SerializeField] SoundsSO _soundsSO;

        void SendSoundsSequenceCommand()
        {
            InvokeCommand(0, _soundsSO);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            SendSoundsSequenceCommand();
        }
    }
}