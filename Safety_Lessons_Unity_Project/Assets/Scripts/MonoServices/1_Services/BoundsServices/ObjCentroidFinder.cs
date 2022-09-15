using UnityEngine;

namespace MonoServices.MeshBounds
{
    public class ObjCentroidFinder : MonoBehaviour
    {
        Vector3 _boundsSize;
        Vector3 _centroid;
        Bounds _bounds;

        public Bounds Bounds => _bounds;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_centroid, _boundsSize);
        }

        Vector3 FindObjCentriod(MeshRenderer[] meshRenderers)
        {
            var centroid = Vector3.zero;
            var pointsNum = meshRenderers.Length;

            foreach (var meshRenderer in meshRenderers)
                centroid += meshRenderer.bounds.center;

            centroid /= pointsNum;

            return centroid;
        }

        Vector3 GetBoundsSize(MeshRenderer[] meshRenderers)
        {
            Bounds bounds = new Bounds(transform.position, Vector3.zero);

            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                bounds.Encapsulate(renderer.bounds);

            return bounds.size;
        }
    }
}