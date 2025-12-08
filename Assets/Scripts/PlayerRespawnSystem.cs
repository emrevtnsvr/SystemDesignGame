using Unity.Cinemachine;
using UnityEngine;

public class PlayerRespawnSystem : MonoBehaviour
{
    public Transform checkpoint;


    private Rigidbody rb;
    private Vector3 startPosition;


    public Vector3 checkpointPosition;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;


        checkpointPosition = checkpoint != null ? checkpoint.position : startPosition;
    }


    public void RespawnAtCheckpoint()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = checkpointPosition + Vector3.up * 1f;
        transform.rotation = Quaternion.LookRotation(-Vector3.forward);


        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            vcam.OnTargetObjectWarped(transform, transform.position - startPosition);
        }


        Debug.Log("Respawned to checkpoint!");
    }


    public void RespawnPlayer()
    {
        transform.position = checkpointPosition;
        rb.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(-Vector3.forward);

        GetComponent<MovementSystem>().RestoreSpeed();
        GetComponent<PlayerLifeSystem>().ResetLives();

        FindObjectOfType<BlackHoleMovement>()?.ResetToStart();
        FindObjectOfType<ObstacleSystem>()?.ResetObstacles(); // ✅ RESET OBSTACLES

        FindObjectOfType<ScoreSystem>()?.ResetScore();
        FindObjectOfType<ScoreSystem>()?.StartScoring();

        Debug.Log("Player respawned.");
    }
}

