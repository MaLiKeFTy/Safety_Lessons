using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformParentRotationFollower : MonoBehaviour
    {
        [SerializeField] float _rotDistance = 5;

        float _previousXRotation;
        float _previousYRotation;

        void Start()
        {
            _previousXRotation = transform.parent.eulerAngles.x;
            _previousYRotation = transform.parent.eulerAngles.y;
        }

        void FixedUpdate()
        {
            var parentRotXAxis = ParentRotAxisValue(transform.parent.eulerAngles.x, ref _previousXRotation);
            var parentRotYAxis = ParentRotAxisValue(transform.parent.eulerAngles.y, ref _previousYRotation);

            var targetRot = Quaternion.Euler(parentRotXAxis * _rotDistance, parentRotYAxis * _rotDistance, 0);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime);
        }

        float ParentRotAxisValue(float currAxisValue, ref float previousAxisValue)
        {
            float currentYRotation = currAxisValue;

            float parentRotAxisValue = Mathf.Clamp(currAxisValue - previousAxisValue, -1f, 1f);
            previousAxisValue = currentYRotation;


            return parentRotAxisValue;
        }

    }
}
