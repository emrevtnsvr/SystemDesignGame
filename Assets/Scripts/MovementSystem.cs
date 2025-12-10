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
    public CameraTargetMover targetMover;
    private Rigidbody rb;
    private PlayerInputSystem input;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInputSystem>();

        currentForwardSpeed = baseForwardSpeed;
        currentStrafeSpeed = baseStrafeSpeed;


    }
    private void Start()
    {
        targetMover.SetSpeed(baseForwardSpeed);
        targetMover.SetPosition(transform);
    }
    public void Reset()
    {
        targetMover.SetPosition(transform);
    }
    void FixedUpdate()
    {
        Vector3 forward = transform.forward * currentForwardSpeed;
        Vector3 strafe = -transform.right * input.horizontalInput * currentStrafeSpeed;

        Vector3 velocity = new Vector3(strafe.x, rb.linearVelocity.y, forward.z);
        rb.linearVelocity = velocity;
       // targetMover.SetSpeed(velocity.magnitude);
        // ❗ Pozisyon limiti uygula
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    // Can kaybında geçici yavaşlama
    public void SetSlowdown(float multiplier)
    {
        currentForwardSpeed = baseForwardSpeed * multiplier;
        currentStrafeSpeed = baseStrafeSpeed * multiplier;
        targetMover.SetSpeed(baseForwardSpeed);
    }

    public void RestoreSpeed()
    {
        currentForwardSpeed = baseForwardSpeed;
        currentStrafeSpeed = baseStrafeSpeed;
        targetMover.SetSpeed(baseForwardSpeed);
    }


    public void ResetSpeed()
    {
        baseForwardSpeed = 5f;
        baseStrafeSpeed = 5f;

        currentForwardSpeed = baseForwardSpeed;
        targetMover.SetSpeed(baseForwardSpeed);
        currentStrafeSpeed = baseStrafeSpeed;
    }

    // ✅ Skora göre kalıcı hız artışı
    public void IncreaseSpeed(float multiplier)
    {
        baseForwardSpeed *= multiplier;
        baseStrafeSpeed *= multiplier;

        currentForwardSpeed = baseForwardSpeed;
        currentStrafeSpeed = baseStrafeSpeed;
        targetMover.SetSpeed(baseForwardSpeed);
        Debug.Log("💨 Speed increased! Forward: " + baseForwardSpeed + ", Strafe: " + baseStrafeSpeed);
    }
}

