using System.Collections.Generic;
using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Animations
{
    public class AnimationEventsHandler : ParametersHandler
    {
        [SerializeField] List<AnimationClipInfoSO> _animEvents = new List<AnimationClipInfoSO>();

        public List<string> EventNames => ParameterNames();

        public override bool isInvoker => true;

        protected override void Start()
        {
            base.Start();

            foreach (var item in _animEvents)
            {
                for (int j = 0; j < item.AnimClipEventTimes.Count; j++)
                {
                    AnimationEvent evnt = new AnimationEvent();

                    evnt.stringParameter = item.AnimClipEventTimes[j].EventName;

                    int seconds = (int)item.AnimClipEventTimes[j].EventTime;
                    float milliseconds = item.AnimClipEventTimes[j].EventTime - seconds;

                    if (item.AnimClip.frameRate > 100)
                        item.AnimClip.frameRate = 100;

                    float calculatedSeconds = seconds + (milliseconds / (item.AnimClip.frameRate / 100));

                    evnt.time = calculatedSeconds;
                    evnt.functionName = nameof(CallAnimationEvent);

                    item.AnimClip.AddEvent(evnt);
                }
            }

        }

        void CallAnimationEvent(string index)
        {
            int eventNameIndex = 0;

            for (int i = 0; i < EventNames.Count; i++)
                if (EventNames[i] == index)
                    eventNameIndex = i;

            InvokeCommand(eventNameIndex, null);
        }


        public override List<string> ParameterNames()
        {
            List<string> tempEventNames = new List<string>();

            foreach (var animEventHolder in _animEvents)
                foreach (var animEvent in animEventHolder.AnimClipEventTimes)
                    if (!tempEventNames.Contains(animEvent.EventName))
                        tempEventNames.Add(animEvent.EventName);

            return tempEventNames;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}