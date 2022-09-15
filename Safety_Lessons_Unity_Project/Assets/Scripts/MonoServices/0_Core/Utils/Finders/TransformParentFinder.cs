using UnityEngine;

namespace MonoServices.Core
{
    public static class TransformParentFinder
    {
        public static Transform TranformParent(Transform startingTranform, int parentNumb)
        {
            for (int i = 0; i < parentNumb; i++)
                if (startingTranform.parent)
                    startingTranform = startingTranform.parent;

            return startingTranform;
        }

    }
}

