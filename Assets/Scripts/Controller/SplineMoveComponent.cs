using Scripts.Path;
using UnityEngine;

namespace Scripts.Controller
{
    public delegate void OnSplineMovementFinished();

    [RequireComponent(typeof(MeshFilter))]
    public class SplineMoveComponent : MonoBehaviour
    {
        [SerializeField]
        private BezierSpline spline;

        [SerializeField]
        private MeshFilter meshFilter;

        private float duration;
        private float splineLength;
        public float speed;

        [SerializeField]
        private float progress;

        [SerializeField]
        private float rayLength = 1.0f;

        public event OnSplineMovementFinished OnSplineMovementFinished;

        public float CurrentMoveSpeed => spline.GetVelocity(Progress).magnitude;

        public float Progress
        {
            get => progress;
            private set => progress = value;
        }

        private void Start()
        {
            duration = Bezier.CalculateLength(spline);
            meshFilter = GetComponent<MeshFilter>();

            if (meshFilter != null)
            {
                Debug.Log(meshFilter.mesh.bounds.size.z);
                rayLength = meshFilter.mesh.bounds.size.z / 2f;
            }
        }

        private void OnValidate()
        {
            duration = Bezier.CalculateLength(spline);
        }

        private void Finish()
        {
            if (Progress > 1f)
            {
                Progress = 0f;
                OnSplineMovementFinished?.Invoke();
            }
        }

        private bool IsPathBlocked(Vector3 position, Vector3 direction)
        {
            RaycastHit hit;
            var ray = new Ray(position, direction);
            var collisionMask = 1 << 3;

            return Physics.Raycast(ray, out hit, rayLength, collisionMask);
        }

        private void Update()
        {
            var position = spline.GetPoint(Progress);
            var direction = spline.GetDirection(Progress);

            if (IsPathBlocked(position, direction))
            {
                return;
            }

            Progress += Time.deltaTime / (duration / speed);

            transform.localPosition = position;
            transform.LookAt(position + direction);

            Finish();
        }
    }
}
