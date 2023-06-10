using Scripts.Path;
using Scripts.Building;
using UnityEngine;
using Scripts.Utils;

namespace scripts.controller
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private BuildPreview previewPrefab;

        private bool GetIntersection(out RaycastHit hit, int layerMask)
        {
            var cam = Camera.main;
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out hit, 1000f, layerMask);
        }

        private SplineCollider OnPathHit(out RaycastHit hit)
        {
            if (GetIntersection(out hit, CollisionMask.PATH) && hit.collider)
            {
                return hit.collider.GetComponent<SplineCollider>();
            }

            return null;
        }

        public void PlaceItem()
        {
            if (OnPathHit(out var hit) is var splineCollider && splineCollider)
            {
                var forward = splineCollider.GetTriangleForward(hit.triangleIndex);

                previewPrefab.transform.position = hit.point;
                previewPrefab.transform.rotation = Quaternion.LookRotation(-forward, Vector3.up);

                if (previewPrefab.CanBuild())
                {
                    previewPrefab.SetColor(Color.green);
                }
                else
                {
                    previewPrefab.SetColor(Color.red);
                }
            }
        }

        public void Update()
        {
            PlaceItem();
            if (Input.GetMouseButtonDown(0)) { }
        }
    }
}
