using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class LookAtObjHandler : MonoService
    {
        [SerializeField] Vector3 _offset;

        [SerializeField, Range(0.1f, 1)] float _moveSpeed = 0.5f;
        [SerializeField] bool _lookAtX, _lookAtY = true, _lookAtZ;

        [SerializeField] AnimationCurve _speedCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] bool _lookAtMainCam = true;
        [SerializeField] bool _constantLooking;

        [SerializeField] Transform target;
        Camera _mainCam;
        bool _isLooking;
        Quaternion _intialRot;
        bool _lookedOnce;

        protected override void Start()
        {
            base.Start();

            _mainCam = Camera.main;
        }

        void ToggleLookingCommand()
        {
            ActivateCoroutine(Rotating());
        }

        void FinishedRotatingCommand()
        {
            InvokeCommand(1);
        }

        IEnumerator Rotating()
        {
            float currentLerpTime = 0;
            float targetLerpTime = 1;

            if (!_isLooking)
                _intialRot = transform.rotation;

            _isLooking = !_isLooking;

            if (_lookAtMainCam)
                target = _mainCam.transform;

            var lookPos = target.position - transform.position;

            if (_lookAtX) lookPos.x = 0;
            if (_lookAtY) lookPos.y = 0;
            if (_lookAtZ) lookPos.z = 0;

            var rotation = _isLooking ? Quaternion.LookRotation(lookPos) : _intialRot;


            while (currentLerpTime != targetLerpTime)
            {
                currentLerpTime = Mathf.MoveTowards(currentLerpTime, targetLerpTime, _moveSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speedCurve.Evaluate(currentLerpTime));

                yield return null;
            }

            FinishedRotatingCommand();

            yield return null;
        }

        void Update()
        {
            ContantLooking();
        }

        void ContantLooking()
        {
            if (!_constantLooking)
                return;

            if (_lookAtMainCam)
                target = _mainCam.transform;

            var lookPos = (target.position + _offset) - transform.position;

            if (_lookAtX) lookPos.x = 0;
            if (_lookAtY) lookPos.y = 0;
            if (_lookAtZ) lookPos.z = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), _moveSpeed * 10 * Time.deltaTime);
        }



        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ToggleLookingCommand();
            if (methodNumb == 2) TurnOffConstantLookingCommand();
            if (methodNumb == 3) LookAtTargetOnceCommand();
        }

        void TurnOffConstantLookingCommand()
        {
            _constantLooking = false;
        }

        void LookAtTargetOnceCommand()
        {
            if (_lookedOnce)
                return;

            if (_lookAtMainCam)
                target = _mainCam.transform;

            var lookPos = target.position - transform.position;

            if (_lookAtX) lookPos.x = 0;
            if (_lookAtY) lookPos.y = 0;
            if (_lookAtZ) lookPos.z = 0;

            transform.rotation = Quaternion.LookRotation(lookPos);

            _lookedOnce = true;
        }


    }
}