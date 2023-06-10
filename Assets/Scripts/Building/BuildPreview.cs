using UnityEngine;

namespace Scripts.Building
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class BuildPreview : MonoBehaviour
    {
        private IStructure structure;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        [SerializeField]
        private GameObject preview;

        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            structure = preview.GetComponent<IStructure>();
        }

        public void SetPreview(IStructure structure)
        {
            var (meshFilter, meshRenderer) = structure.GetMesh();

            this.meshFilter = meshFilter;
            this.meshRenderer = meshRenderer;
            this.structure = structure;
        }

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;
        }

        public bool CanBuild() => structure.CanBuild(transform);
    }
}
