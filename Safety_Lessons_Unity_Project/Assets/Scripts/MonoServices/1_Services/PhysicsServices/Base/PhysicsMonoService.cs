using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoPhysics
{
    public abstract class PhysicsMonoService : MonoService
    {
        [Space, SerializeField] protected string[] _PhysicsInteractionObjectTags;


        protected override void Awake()
        {
            base.Awake();
        }

        protected bool IsRightTag(Transform tansTag)
        {
            bool isRightTag = false;

            foreach (var monoService in tansTag.GetComponentsInChildren<PhysicsInteractionObject>())
                foreach (var filteredTag in _PhysicsInteractionObjectTags)
                    if (monoService.ObjectTag == filteredTag)
                    {
                        isRightTag = true;
                        monoService.OnInteractedObjCommand();
                    }



            return isRightTag;
        }
    }
}
