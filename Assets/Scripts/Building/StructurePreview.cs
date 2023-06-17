using UnityEngine;

namespace Scripts.Building
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class StructurePreview : MonoBehaviour
    {
        [SerializeField]
        private Structure structure;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        [Header("Preview Settings")]
        [SerializeField]
        private float transparency = 0.35f;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            SetPrefab(structure);
        }

        public void SetPrefab(Structure structure)
        {
            var (meshFilter, meshRenderer) = structure.GetMesh();

            this.meshFilter.sharedMesh = meshFilter.sharedMesh;
            this.structure = structure;
        }

        public void SetActive(bool active) => gameObject.SetActive(active);

        public bool IsActive() => gameObject.activeSelf;

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public GameObject GetPrefab() => structure.gameObject;

        public void SetColor(Color color)
        {
            meshRenderer.sharedMaterial.color = new Color(color.r, color.g, color.b, transparency);
        }

        public bool CanBuild()
        {
            return structure.CanBuild(transform);
        }
    }
}
