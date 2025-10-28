using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Jump Attributes")]
    public float jumpForce = 6.5f;
    public float gravityScale = 2.0f;
    public bool isGrounded = false;

    public ParticleSystem jetBurstVFX;
    public AudioSource jumpSFX;
    public Camera mainCamera;

    private float verticalVelocity;
    private Vector3 jumpDirection = Vector3.up;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            ExecuteJump();
        }

        if (!isGrounded)
        {
            verticalVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
            rb.linearVelocity += Vector3.up * verticalVelocity * Time.deltaTime;
        }
    }

    void ExecuteJump()
    {
        rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        isGrounded = false;

        if (jetBurstVFX != null) jetBurstVFX.Play();
        if (jumpSFX != null) jumpSFX.Play();

        if (mainCamera != null)
            mainCamera.transform.rotation *= Quaternion.Euler(-2.5f, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            verticalVelocity = 0f;
        }
    }
}
