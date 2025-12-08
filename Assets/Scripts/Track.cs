using UnityEngine;

public class Track : MonoBehaviour
{
    public Transform TrackObject;
    public Transform Player;

    void LateUpdate()
    {
        // Kamera hedefi = Player'ın biraz arkası
        Vector3 followPos = Player.position;
        followPos.z -= 8f;  // Kameranın arkaya offset'i
        followPos.y += 3f;  // Kameranın yukarı offset'i

        TrackObject.position = followPos;

        // Kameranın bakacağı yön
        TrackObject.rotation = Quaternion.LookRotation(Player.forward);
    }
}
