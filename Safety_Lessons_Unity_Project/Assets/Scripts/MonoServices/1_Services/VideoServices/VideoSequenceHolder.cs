using MonoServices.Core;
using UnityEngine;

namespace MonoSevices.Videos
{
    public class VideoSequenceHolder : MonoService
    {
        [SerializeField] VideoSequence _videoSequence;

        void SendVideoSequenceCommand()
        {
            InvokeCommand(0, _videoSequence);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            SendVideoSequenceCommand();
        }
    }
}
