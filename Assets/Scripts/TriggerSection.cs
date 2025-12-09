using UnityEngine;

public class TriggerSection : MonoBehaviour
{
    public GameObject roadSection;

    private float GetRoadLength()
    {
        // Prefabın tüm mesh’lerinin bounding box’ını hesaplar    
        MeshFilter[] meshes = roadSection.GetComponentsInChildren<MeshFilter>();

        float minZ = float.MaxValue;
        float maxZ = float.MinValue;

        foreach (MeshFilter mf in meshes)
        {
            Bounds b = mf.sharedMesh.bounds;
            Vector3 worldMin = mf.transform.TransformPoint(b.min);
            Vector3 worldMax = mf.transform.TransformPoint(b.max);

            minZ = Mathf.Min(minZ, worldMin.z, worldMax.z);
            maxZ = Mathf.Max(maxZ, worldMin.z, worldMax.z);
        }

        return Mathf.Abs(maxZ - minZ);  // Gerçek yol uzunluğu
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float length = GetRoadLength();

            Vector3 spawnPos = transform.root.position + new Vector3(0, 0, -length);
            Instantiate(roadSection, spawnPos, Quaternion.identity);
        }
    }
}
