using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputSystem))]
public class MovementSystem : MonoBehaviour
{
    public float baseForwardSpeed = 5f;
    public float baseStrafeSpeed = 5f;

    public float minX = -5f;
    public float maxX = 5f;

    private float currentForwardSpeed;
    private float currentStrafeSpeed;

    private Rigidbody rb;
    private PlayerInputSystem input;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInputSystem>();

        currentForwardSpeed = baseForwardSpeed;
        currentStrafeSpeed = baseStrafeSpeed;
    }

    void FixedUpdate()
    {
        Vector3 forward = transform.forward * currentForwardSpeed;
        Vector3 strafe = -transform.right * input.horizontalInput * currentStrafeSpeed;

        Vector3 velocity = new Vector3(strafe.x, rb.linearVelocity.y, forward.z);
        rb.linearVelocity = velocity;

        // ❗ Pozisyon limiti uygula
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    // Bu fonksiyon can kaybında yavaşlama için kullanılacak
    public void SetSlowdown(float multiplier)
    {
        currentForwardSpeed = baseForwardSpeed * multiplier;
        currentStrafeSpeed = baseStrafeSpeed * multiplier;
    }

    // Bu fonksiyon 2 saniye sonra hızı geri getirecek
    public void RestoreSpeed()
    {
        currentForwardSpeed = baseForwardSpeed;
        currentStrafeSpeed = baseStrafeSpeed;
    }
}

