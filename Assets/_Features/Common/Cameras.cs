
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class Cameras
    {
        public static bool IsObjectVisibleInCamera(GameObject obj, Camera cam)
        {
            Renderer objectRenderer = obj.GetComponent<Renderer>();
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

            return GeometryUtility.TestPlanesAABB(planes, objectRenderer.bounds);
        }
    }
}