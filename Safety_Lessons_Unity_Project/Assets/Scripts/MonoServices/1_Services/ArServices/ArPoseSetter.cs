using UnityEngine;
using MonoServices.Core;

namespace MonoServices.AR
{
    public class ArPoseSetter : MonoService
    {
        [SerializeField] bool _canSetPos = true;

        [SerializeField] int parentLevelIndexToMove = 0;
        [SerializeField] Vector3 _positionOffset;
        [SerializeField] Vector3 _rotationOffset;
        [SerializeField] bool _applyRotations = true;

        Pose _poseToSet;

        Transform transToMove;

        protected override void Start()
        {
            base.Start();

            transToMove = TransformParentFinder.TranformParent(transform, parentLevelIndexToMove);
        }

        void GetPoseCommand(Pose poseWithScale)
        {
            _poseToSet = poseWithScale;
            InvokeCommand(0, poseWithScale);
        }

        void SetPoseCommand()
        {
            if (!_canSetPos)
                return;


            if (_poseToSet == null || !transToMove)
                return;


            var rotation = _applyRotations ? _poseToSet.rotation : Quaternion.identity;

            transToMove.SetPositionAndRotation(_poseToSet.position + _positionOffset, rotation * Quaternion.Euler(_rotationOffset));
            InvokeCommand(1);
        }

        void GetAndSetPoseCommand(Pose pose)
        {
            _poseToSet = pose;

            GetPoseCommand(pose);
            SetPoseCommand();

            InvokeCommand(2, pose);
        }

        void SendPoseCommand()
        {

            Pose thisPose = new Pose(transToMove.transform.position, transToMove.transform.rotation);

            InvokeCommand(3, thisPose);
        }

        void TurnOffSetPoseCommand()
        {
            _canSetPos = false;
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {

            if (methodNumb == 1) SetPoseCommand();

            if (methodNumb == 3) SendPoseCommand();
            if (methodNumb == 4) TurnOffSetPoseCommand();

            if (passedObj == null)
                return;

            if (methodNumb == 0) GetPoseCommand((Pose)passedObj);
            if (methodNumb == 2) GetAndSetPoseCommand((Pose)passedObj);


        }
    }
}
