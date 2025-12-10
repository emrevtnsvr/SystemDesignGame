using UnityEngine;
[RequireComponent (typeof(Rigidbody))]
public class CameraTargetMover : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint; 
    [SerializeField] Vector3 offset = new Vector3(0, 3, 3);
    [SerializeField] float speed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 direction;
    private Rigidbody rb;
    void Awake()
    {
        direction = endPoint.position - startPoint.position;
        direction.Normalize();
        transform.position = startPoint.position;
        rb = GetComponent<Rigidbody>();
    }
    public void SetPosition(Transform gameObject)
    {
        rb.position = gameObject.position + offset;
        Debug.Log($"SetTargetPos: {gameObject.position + offset}");
    }
    public void SetSpeed(float speed)
    {
        rb.linearVelocity = speed * direction;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       // transform.position += direction * speed * Time.deltaTime;
    }
}
