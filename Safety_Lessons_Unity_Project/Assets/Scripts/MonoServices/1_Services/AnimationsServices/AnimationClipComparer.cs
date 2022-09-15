using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimationClipComparer : ParametersHandler
    {

        [SerializeField] AnimationClip[] _animClips;

        public override bool isInvoker => false;

        void CompareAnimCommand(AnimationClip passedAnimClip)
        {
            for (int i = 0; i < _animClips.Length; i++)
            {
                var animClip = _animClips[i];

                if (animClip == passedAnimClip)
                    InvokeCommand(i);
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            CompareAnimCommand((AnimationClip)passedObj);
        }

        public override List<string> ParameterNames()
        {
            List<string> parameterNames = new List<string>();

            foreach (var animClip in _animClips)
                parameterNames.Add($"On {animClip.name}");

            return parameterNames;
        }
    }
}
