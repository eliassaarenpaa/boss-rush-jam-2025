using UnityEngine;

namespace Project.Runtime.Core.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 TransformToCameraSpace(this Camera camera, Vector3 worldSpaceVector)
        {
            var t = camera.transform;
            var forward = t.forward;
            var right = t.right;
            forward.y = 0;
            right.y = 0;

            var dir = forward * worldSpaceVector.z + right * worldSpaceVector.x;
            return dir.normalized;
        }
    }
}
