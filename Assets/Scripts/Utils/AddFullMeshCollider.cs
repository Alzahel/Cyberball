using UnityEngine;

namespace Utils
{
    public class AddFullMeshCollider : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            AddMeshCollider(gameObject);
        }
    
        /// <summary>
        /// Add mesh collider to game object
        /// Gets all child components, looks for meshes and assings
        /// them to gameobject meshcollider
        /// </summary>
        /// <param name="containerModel">The gameobject</param>
        private void AddMeshCollider (GameObject containerModel)
        {
            // Add mesh collider
            MeshFilter meshFilter = containerModel.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                MeshCollider meshCollider = containerModel.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = meshFilter.sharedMesh;
            }
            // Add mesh collider (convex) for each mesh in child elements.
            Component[] meshes = containerModel.GetComponentsInChildren<MeshFilter>();
            foreach (MeshFilter mesh in meshes)
            {
                MeshCollider meshCollider = containerModel.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = mesh.sharedMesh;
            }
        }
    }
}
