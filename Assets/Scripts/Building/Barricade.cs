using UnityEngine;
using Scripts.Utils;

namespace Scripts.Building
{
    [RequireComponent(typeof(BoxCollider))]
    public class Barricade : Structure
    {
        private BoxCollider boxCollider;

        protected override void Awake()
        {
            base.Awake();

            boxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        private (Vector3 left, Vector3 right) CalculateRaycasts(Transform transform)
        {
            var bounds = meshFilter.sharedMesh.bounds;
            var position = transform.position;
            var rotation = transform.rotation;

            var left = position + rotation * new Vector3(-bounds.extents.x, 0f, 0f);
            var right = position + rotation * new Vector3(bounds.extents.x, 0f, 0f);

            left += Vector3.up * 0.5f;
            right += Vector3.up * 0.5f;

            return (left, right);
        }

        private bool IsNotColliding(Transform transform)
        {
            Awake();

            return !Physics.BoxCast(
                transform.position + Vector3.up * 10f,
                boxCollider.size / 2,
                Vector3.down,
                transform.rotation,
                10f,
                CollisionMask.BARRICADE | CollisionMask.ENEMY
            );
        }

        private bool CastRay(Vector3 direction) =>
            Physics.Raycast(direction, Vector3.down, 10f, CollisionMask.PATH);

        private void DrawBounds(Transform transform)
        {
            var rays = CalculateRaycasts(transform);

            Debug.DrawLine(rays.left, rays.left + Vector3.down * 10f, Color.red, 10f);
            Debug.DrawLine(rays.right, rays.right + Vector3.down * 10f, Color.red, 10f);
        }

        public override bool CanBuild(Transform t)
        {
            return IsNotColliding(t)
                && CalculateRaycasts(t) is var rays
                && (CastRay(rays.left) && CastRay(rays.right));
        }
    }
}
