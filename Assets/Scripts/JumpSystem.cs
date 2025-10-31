using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class JumpSystem : MonoBehaviour
{

    public float desiredJumpHeight = 3.2f;
    public float timeToApex = 0.50f;

    [Header("Hang-Time & Feel")]
    public float apexThresholdY = 0.5f;
    public float ascendMult = 1.00f;
    public float apexMult = 0.35f;
    public float fallMult = 1.60f;
    public float cutJumpMult = 2.0f;

    [Header("Events")]
    public UnityEvent onJumpStart;
    public UnityEvent onJumpEnd;

    Rigidbody rb;
    bool isGrounded = true;
    bool isJumping = false;

    float baseGravity;
    float jumpVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        baseGravity = -2f * desiredJumpHeight / (timeToApex * timeToApex);
        jumpVelocity = 2f * desiredJumpHeight / timeToApex;
    }

    void Update()
    {
        // Jump start
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            DoJump();
            onJumpStart?.Invoke();
            isJumping = true;
        }

        // Space bırakıldığında (her durumda)
        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            onJumpEnd?.Invoke();
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        var v = rb.linearVelocity;
        float gMult;

        bool spacePressed = Input.GetKey(KeyCode.Space);
        float vy = v.y;

        if (Mathf.Abs(vy) <= apexThresholdY)
            gMult = apexMult;
        else if (vy > apexThresholdY)
        {
            gMult = ascendMult;
            if (!spacePressed) gMult *= cutJumpMult;
        }
        else
            gMult = fallMult;

        rb.AddForce(Vector3.up * baseGravity * gMult, ForceMode.Acceleration);
    }

    void DoJump()
    {
        var v = rb.linearVelocity;
        v.y = jumpVelocity;
        rb.linearVelocity = v;
        isGrounded = false;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            // yere değince sesi KES 🔥 (garanti sonlandırma)
            if (isJumping)
            {
                onJumpEnd?.Invoke();
                isJumping = false;
            }
        }
    }
}
