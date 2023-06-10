using UnityEngine;

namespace Scripts.Building
{
    public interface IStructure
    {
        bool CanBuild(Transform transform);
        (MeshFilter filter, MeshRenderer renderer) GetMesh();
    }
}
