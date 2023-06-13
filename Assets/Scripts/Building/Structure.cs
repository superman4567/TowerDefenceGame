using UnityEngine;

namespace Scripts.Building
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [System.Serializable]
    public abstract class Structure : MonoBehaviour
    {
        [Header("Structure Information")]
        [SerializeField]
        private int value;

        [SerializeField]
        private string structureName;

        [SerializeField]
        [TextArea(3, 5)]
        private string description;

        protected MeshFilter meshFilter;
        protected MeshRenderer meshRenderer;

        protected virtual void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public virtual bool CanBuild(Transform t) => true;

        public (MeshFilter, MeshRenderer) GetMesh() => (meshFilter, meshRenderer);
    }
}
