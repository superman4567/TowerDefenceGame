using UnityEngine;
using Scripts.Utils;

namespace Scripts.Building
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Barricade : MonoBehaviour, IStructure
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private (Vector3 left, Vector3 right) CalculateRaycasts(Transform transform)
        {
            var bounds = meshFilter.mesh.bounds;
            var position = transform.position;
            var rotation = transform.rotation;

            var left = position + rotation * new Vector3(-bounds.extents.x, 0f, 0f);
            var right = position + rotation * new Vector3(bounds.extents.x, 0f, 0f);

            left += Vector3.up * 0.5f;
            right += Vector3.up * 0.5f;

            return (left, right);
        }

        private bool CastRay(Vector3 direction) =>
            Physics.Raycast(direction, Vector3.down, 10f, CollisionMask.PATH);

        private void DrawBounds(Transform transform)
        {
            var rays = CalculateRaycasts(transform);

            Debug.DrawLine(rays.left, rays.left + Vector3.down * 10f, Color.red, 10f);
            Debug.DrawLine(rays.right, rays.right + Vector3.down * 10f, Color.red, 10f);
        }

        public bool CanBuild(Transform t)
        {
            return CalculateRaycasts(t) is var rays && (CastRay(rays.left) && CastRay(rays.right));
        }

        public (MeshFilter filter, MeshRenderer renderer) GetMesh()
        {
            return (meshFilter, meshRenderer);
        }
    }
}
