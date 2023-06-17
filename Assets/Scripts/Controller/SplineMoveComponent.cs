using Scripts.Path;
using UnityEngine;

namespace Scripts.Controller
{
    public delegate void OnSplineMovementFinished();

    [RequireComponent(typeof(MeshFilter))]
    public class SplineMoveComponent : MonoBehaviour
    {
        [Header("Spline Settings")]
        [SerializeField]
        private BezierSpline spline;

        [SerializeField]
        private float progress;

        [Header("Movement Settings")]
        [SerializeField]
        private float maxMovementSpeed = 5f;

        [SerializeField]
        private float movementSpeed = 0f;

        [SerializeField]
        private float interpolationSpeed = 0.001f;
        private float duration;
        private float splineLength;
        private MeshFilter meshFilter;

        [Header("Collision Settings")]
        [SerializeField]
        private float rayLength = 1.0f;

        [SerializeField]
        private float collisionMargin = 0.001f;

        [SerializeField]
        private bool debugMode;

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

            if (debugMode)
            {
                Debug.DrawLine(position, position + direction * rayLength, Color.red, 0.1f);
            }

            return Physics.Raycast(ray, out hit, rayLength + collisionMargin, collisionMask);
        }

        private float InterpolatedSpeed(float speed)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, speed, interpolationSpeed);
            return movementSpeed;
        }

        private void Update()
        {
            var position = spline.GetPoint(Progress);
            var direction = spline.GetDirection(Progress);
            var origin = position + new Vector3(0f, meshFilter.mesh.bounds.size.y / 2f, 0f);
            var increment = Time.deltaTime / (duration / maxMovementSpeed);

            if (IsPathBlocked(origin, direction))
            {
                movementSpeed = 0f;
                return;
            }

            movementSpeed = InterpolatedSpeed(increment);
            Progress += movementSpeed;
            transform.localPosition = new Vector3(
                position.x,
                transform.localPosition.y,
                position.z
            );

            var lookDir = transform.position + direction;
            Debug.Log(lookDir);
            transform.LookAt(lookDir);
            Finish();
        }
    }
}
