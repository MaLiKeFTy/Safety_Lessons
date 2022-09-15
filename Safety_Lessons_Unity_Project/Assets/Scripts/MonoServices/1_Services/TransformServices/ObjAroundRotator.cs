using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class ObjAroundRotator : TransformMonoService
    {
        [SerializeField] float _speed = 22;
        [SerializeField] float _radius = 5;
        [SerializeField] Transform _target;
        [SerializeField] bool _shouldRotate;

        protected override void Start()
        {
            base.Start();
            ActivateRotation();
        }

        void ActivateRotation()
        {
            StartCoroutine(RotatingCoroutine());
            InvokeCommand(0);
        }

        IEnumerator RotatingCoroutine()
        {
            while (true)
            {
                Rotating();
                yield return null;
            }
        }

        void Rotating()
        {
            if (!_shouldRotate) return;

            var lookDir = Vector3.Cross(_target.position - _ThisTransform.position, -Vector3.up);
            _ThisTransform.rotation = Quaternion.Lerp(_ThisTransform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), _speed * Time.deltaTime);
            _ThisTransform.RotateAround(_target.position, -Vector3.up, _speed * Time.deltaTime);

            var distance = Vector3.Distance(transform.position, _target.position);
            var radiusComparison = _radius - distance;
            var targetPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);

            if (Mathf.Abs(radiusComparison) <= 2f)
                return;

            if (radiusComparison <= 2f)
                transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
            else
                transform.position = Vector3.MoveTowards(transform.position, targetPos, -_speed * Time.deltaTime);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartRotatingCommand();
            if (methodNumb == 1) StopRotatingCommand();
            if (methodNumb == 2) GetRadiusCommand((float)passedObj);
        }

        void StartRotatingCommand() =>
            _shouldRotate = true;

        void StopRotatingCommand() =>
            _shouldRotate = false;

        void GetRadiusCommand(float radius) =>
            _radius = radius;

        void OnDrawGizmosSelected()
        {
            if (!_target)
                return;

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(_target.position, _radius);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_target.position, transform.position);
        }
    }
}