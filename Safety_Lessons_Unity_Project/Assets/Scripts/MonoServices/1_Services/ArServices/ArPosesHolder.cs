using System.Collections.Generic;
using UnityEngine;
using MonoServices.Core;

namespace MonoServices.AR
{
    public class ArPosesHolder : MonoService
    {
        [Space, SerializeField] List<Pose> _poses = new List<Pose>();

        Pose _currPoseToSet;

        void SetPoseListCountCommand(int listCount)
        {
            _poses.Clear();

            for (int i = 0; i < listCount; i++)
                _poses.Add(new Pose());

            InvokeCommand(0);

        }

        void SetCurrPoseValueCommand(Pose pose)
        {
            _currPoseToSet = pose;
            InvokeCommand(1);
        }

        void AddPoseToListCommand(int indexPlacement)
        {
            _poses[indexPlacement] = _currPoseToSet;
            InvokeCommand(2);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SetPoseListCountCommand((int)passedObj);
            if (methodNumb == 1) SetCurrPoseValueCommand((Pose)passedObj);
            if (methodNumb == 2) AddPoseToListCommand((int)passedObj);
        }
    }
}