using System.Collections;
using UnityEngine;

public class BlackHoleMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float acceleration = 0.2f;

    public Transform startPoint;

    private float currentSpeed;
    private Vector3 startPosition;

    void Start()
    {
        currentSpeed = moveSpeed;
        startPosition = (startPoint != null) ? startPoint.position : transform.position;
    }

    void Update()
    {
        if (!player) return;

        Vector3 lookPos = player.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        transform.position += transform.forward * currentSpeed * Time.deltaTime;
        currentSpeed += acceleration * Time.deltaTime;
    }

    public void ResetToStart()
    {
        transform.position = startPosition;
        currentSpeed = moveSpeed;
        StartCoroutine(DisableColliderTemporarily());
        Debug.Log("🌀 Blackhole reset to START position");
    }

    private IEnumerator DisableColliderTemporarily()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        GetComponent<Collider>().enabled = true;
    }
}
