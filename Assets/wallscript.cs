using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class SolidWall : MonoBehaviour
{
    void Start()
    {
        // Fix visual backface culling
        Material[] materials = GetComponent<Renderer>().materials;
        foreach (Material mat in materials)
        {
            mat.doubleSidedGI = true;
            mat.SetInt("_Cull", 0); // 0 = Off, 1 = Front, 2 = Back
        }
        
        // Ensure collider is properly configured
        Collider collider = GetComponent<Collider>();
        if (collider is MeshCollider meshCollider)
        {
            meshCollider.convex = false;
        }
        
        // Make sure collider is enabled
        collider.enabled = true;
    }
    
    // Optional: Add physics material for bounce/friction control
    void AddPhysicsMaterial()
    {
        PhysicMaterial material = new PhysicMaterial();
        material.bounciness = 0.1f;
        material.dynamicFriction = 0.6f;
        material.staticFriction = 0.6f;
        GetComponent<Collider>().material = material;
    }
}