using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float forwardSpeed = 14f;  // 🔼 arttır, 10–14 arası ideal
    public float lateralSpeed = 7f;   // sağ–sol hareket
    public float lateralClamp = 12f;   // sağ–sol sınır

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 v = rb.linearVelocity;

        // sabit ileri hareket
        v.z = -forwardSpeed;

        // sağ–sol input
        float xInput = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) xInput = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) xInput = 1f;

        v.x = xInput * lateralSpeed;
        rb.linearVelocity = new Vector3(v.x, rb.linearVelocity.y, v.z);

        // sağ–sol sınır
        Vector3 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, -lateralClamp, lateralClamp);
        rb.position = pos;
    }
}
