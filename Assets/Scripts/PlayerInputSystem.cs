using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    public float horizontalInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
}
