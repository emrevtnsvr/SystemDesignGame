using UnityEngine;

public class PlayerRespawnSystem : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; // Başlangıç pozisyonunu kaydet
    }

    public void RespawnPlayer()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = startPosition + Vector3.up * 1f; // Yerden biraz yukarıda spawn
        Debug.Log("Respawned to start position!");
    }
}
