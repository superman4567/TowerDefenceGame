using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Path
{
    [RequireComponent(typeof(BezierSpline), typeof(MeshCollider))]
    public class SplineCollider : MonoBehaviour
    {
        private BezierSpline spline;
        private MeshCollider meshCollider;

        [SerializeField]
        private float step = 0.01f;

        [SerializeField]
        private float width = 1f;

        private Vector3[] triangleForwardDirection;

        private void Start()
        {
            spline = GetComponent<BezierSpline>();
            meshCollider = GetComponent<MeshCollider>();

            GenerateMeshCollider();
        }

        private Vector3 GetRightVector(Vector3 forward)
        {
            return Vector3.Cross(forward, Vector3.up);
        }

        public Vector3 GetTriangleForward(int triangleIndex)
        {
            return triangleForwardDirection[triangleIndex / 2];
        }

        private Vector3[] GenerateVertices()
        {
            float current = 0f;
            var vertices = new List<Vector3>();
            var forwardDirList = new List<Vector3>();

            do
            {
                var p1 = spline.GetPoint(current);
                var forward = spline.GetDirection(current);
                var right = GetRightVector(forward) * width;

                var v1 = (p1 + right) - transform.position;
                var v2 = (p1 - right) - transform.position;

                vertices.Add(v1);
                vertices.Add(v2);
                forwardDirList.Add(forward);
            } while ((current += step) < 1f);

            triangleForwardDirection = forwardDirList.ToArray();
            return vertices.ToArray();
        }

        private int[] GenerateIndices(Vector3[] vertices)
        {
            var indices = new List<int>();

            for (int i = 0; i < vertices.Length - 2; i += 2)
            {
                indices.Add(i);
                indices.Add(i + 2);
                indices.Add(i + 1);

                indices.Add(i + 1);
                indices.Add(i + 2);
                indices.Add(i + 3);
            }

            return indices.ToArray();
        }

        private void GenerateMeshCollider()
        {
            var mesh = new Mesh();

            var vertices = GenerateVertices();
            var indices = GenerateIndices(vertices);

            mesh.vertices = vertices;
            mesh.triangles = indices;

            meshCollider.sharedMesh = mesh;
        }
    }
}
