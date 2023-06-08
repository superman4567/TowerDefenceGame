using Scripts.Path;
using UnityEngine;

namespace Scripts.Controller
{
    public class SplineWalker : MonoBehaviour
    {
        public BezierSpline spline;

        private float duration;

        public float speed;

        public float CurrentMoveSpeed => spline.GetVelocity(Progress).magnitude;

        public float Progress { get; private set; }

        private void Start()
        {
            duration = Bezier.CalculateLength(spline) / speed;
        }

        private void OnValidate()
        {
            duration = Bezier.CalculateLength(spline) / speed;
        }

        private void Finish()
        {
            if (Progress > 1f)
            {
                Progress = 0f;
            }
        }

        private void Update()
        {
            Progress += Time.deltaTime / duration;
            Vector3 position = spline.GetPoint(Progress);

            transform.localPosition = position;
            transform.LookAt(position + spline.GetDirection(Progress));

            Finish();
        }
    }
}
