using UnityEngine;

public class ObstacleSystem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player hit an obstacle!");
            PlayerRespawnSystem respawn = collision.collider.GetComponent<PlayerRespawnSystem>();
            if (respawn != null)
            {
                respawn.RespawnPlayer();
            }
        }
    }   
}
