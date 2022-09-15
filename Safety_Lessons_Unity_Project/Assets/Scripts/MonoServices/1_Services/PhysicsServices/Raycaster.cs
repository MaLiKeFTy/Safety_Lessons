using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.MonoPhysics
{
    public class Raycaster : MonoService
    {
        [SerializeField] LayerMask _layerMask;
        [SerializeField] bool _raycastOnStart;

        bool _isRaycasting;
        float _rayMaxDistance = Mathf.Infinity;
        float _currHitDistance;

        RaycastInteractionObject _currRayObj;

        protected override void Start()
        {
            base.Start();

            if (_raycastOnStart)
                StartRaycastCommand();
        }
        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartRaycastCommand();
            if (methodNumb == 1) StopRaycastCommand();
            if (methodNumb == 5) ChangeRaycastLengthtCommand();
        }

        void StartRaycastCommand()
        {
            _isRaycasting = true;

            StartCoroutine(Raycasting());

            InvokeCommand(0);
        }

        void StopRaycastCommand()
        {
            _isRaycasting = false;

            InvokeCommand(1);
        }

        void RaycastHitCommand(RaycastHit hit)
        {
            InvokeCommand(2, hit.point);
            GetRaycastHitGameObjCommand(hit);
        }

        void RaycastNotHitCommand() =>
            InvokeCommand(3);

        void GetRaycastHitGameObjCommand(RaycastHit hit) =>
            InvokeCommand(4, hit.transform.gameObject);

        void ChangeRaycastLengthtCommand()
        {
            _rayMaxDistance = _currHitDistance;

            InvokeCommand(5);
        }

        void ResetRaycastLenghtCommand()
        {
            _rayMaxDistance = Mathf.Infinity;
        }

        IEnumerator Raycasting()
        {
            while (_isRaycasting)
            {

                bool raycastIsHit = Physics.Raycast(transform.position,
                    transform.TransformDirection(Vector3.forward),
                    out RaycastHit hit,
                    _rayMaxDistance,
                    _layerMask);

                if (raycastIsHit)
                    OnRayHit(hit);
                else
                    OnRayNotHit();

                yield return null;
            }
        }

        void OnRayHit(RaycastHit hit)
        {
            ExitPreviousHittedObj(hit);

            _currRayObj = hit.collider.TryGetComponent(out RaycastInteractionObject hittedObj) ? hittedObj : null;

            if (_currRayObj)
            {
                _currRayObj.OnRaycastEnterCommand();
                _currRayObj.OnRaycastStayCommand();
                RaycastHitCommand(hit);
            }
        }

        void OnRayNotHit()
        {
            if (_currRayObj)
            {
                _currRayObj.OnRaycastExitCommand();
                _currRayObj = null;
            }

            RaycastNotHitCommand();
        }


        void ExitPreviousHittedObj(RaycastHit hit)
        {
            if (!_currRayObj)
                return;

            RaycastInteractionObject hittedRayObj = hit.collider.TryGetComponent(out RaycastInteractionObject hittedObj) ? hittedObj : null;

            if (_currRayObj != hittedRayObj)
                _currRayObj.OnRaycastExitCommand();

        }

    }
}