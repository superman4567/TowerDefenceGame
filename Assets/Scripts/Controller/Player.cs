using UnityEngine;

namespace scripts.controller
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private GameObject towerPrefab;

        private Vector3 GetIntersection()
        {
            RaycastHit hit;
            var cam = Camera.main;
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            return (!Physics.Raycast(ray, out hit)) ? Vector3.zero : hit.point;
        }

        public void PlaceItem()
        {
            var position = GetIntersection();

            Instantiate(towerPrefab, position, Quaternion.identity);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceItem();
            }
        }
    }
}
