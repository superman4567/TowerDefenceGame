using Scripts.Path;
using Scripts.Building;
using UnityEngine;
using Scripts.Utils;

namespace scripts.controller
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private StructurePreview previewStructure;

        private void Start()
        {
            previewStructure = GetComponentInChildren<StructurePreview>();

            if (!previewStructure)
            {
                Debug.LogError("Player is missing StructurePreview component");
                return;
            }

            previewStructure.SetActive(false);
        }

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

        public void BuildStructure()
        {
            var obj = Instantiate(
                previewStructure.GetPrefab(),
                previewStructure.transform.position,
                previewStructure.transform.rotation
            );
        }

        public void SetDefaultPositionRotationAndColor()
        {
            Physics.Raycast(
                Camera.main.ScreenPointToRay(Input.mousePosition),
                out var location,
                1000f
            );

            previewStructure.SetPositionAndRotation(
                location.point,
                Quaternion.LookRotation(-Vector3.forward)
            );

            previewStructure.SetColor(Color.red);
        }

        public bool CanPlaceBuilding()
        {
            if (OnPathHit(out var hit) is var splineCollider && splineCollider)
            {
                var forward = splineCollider.GetTriangleForward(hit.triangleIndex);

                previewStructure.SetPositionAndRotation(
                    hit.point,
                    Quaternion.LookRotation(-forward, Vector3.up)
                );

                if (previewStructure.CanBuild())
                {
                    previewStructure.SetColor(Color.green);
                    return true;
                }
            }

            SetDefaultPositionRotationAndColor();
            return false;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                previewStructure.SetActive(!previewStructure.IsActive());
            }

            if (previewStructure.IsActive() && CanPlaceBuilding())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    BuildStructure();
                }
            }
        }
    }
}
