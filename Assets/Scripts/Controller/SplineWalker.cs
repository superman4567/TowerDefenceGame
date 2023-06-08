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

        private void CalculateConstantSpeed()
        {
            float totalSplineLength = 0f;

            for (int i = 0; i < 1000 - 1; i++)
            {
                var p1 = spline.GetPoint(i / 1000f);
                var p2 = spline.GetPoint((i + 1) / 1000f);

                totalSplineLength += Vector3.Distance(p1, p2);
            }

            duration = totalSplineLength / speed;
        }

        private void Start()
        {
            CalculateConstantSpeed();
        }

        private void OnValidate()
        {
            CalculateConstantSpeed();
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
