using UnityEngine;

namespace MonoServices.Transforms
{
    public class ParentContraintTester : MonoBehaviour
    {
        public Transform Parent;//Remember to assign the parent transform 
        private Vector3 pos, fw, up;

        void Start()
        {
            pos = Parent.transform.InverseTransformPoint(transform.position);
            fw = Parent.transform.InverseTransformDirection(transform.forward);
            up = Parent.transform.InverseTransformDirection(transform.up);
        }
        void Update()
        {
            var newpos = Parent.transform.TransformPoint(pos);
            var newfw = Parent.transform.TransformDirection(fw);
            var newup = Parent.transform.TransformDirection(up);
            var newrot = Quaternion.LookRotation(newfw, newup);
            transform.position = newpos;
            transform.rotation = newrot;
        }
    }
}
