using UnityEngine;

namespace MonoServices.MeshBounds
{
    public static class BoundsHelper
    {

        public static Bounds GetFullObjBounds(Renderer[] renderers)
        {
            Bounds bounds = new Bounds();

            if (renderers.Length > 0)
                bounds = new Bounds(renderers[0].transform.position, renderers[0].transform.localScale);


            foreach (var renderer in renderers)
                bounds.Encapsulate(renderer.bounds);

            return bounds;
        }
    }

}