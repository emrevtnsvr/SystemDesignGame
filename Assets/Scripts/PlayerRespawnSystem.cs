using Unity.Cinemachine;
using UnityEngine;

public class PlayerRespawnSystem : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    public void RespawnPlayer()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = startPosition + Vector3.up * 1f;
        transform.rotation = Quaternion.identity; // player'ı düzleştir

        // Cinemachine kamerayı resetle
        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            vcam.OnTargetObjectWarped(transform, transform.position - startPosition);
        }

        Debug.Log("Respawned to start position!");
    }
}

