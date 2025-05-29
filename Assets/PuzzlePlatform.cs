using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{
    public float moveStep = 2f;
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));
            if (Vector3.Distance(rb.position, targetPosition) < 0.01f)
                isMoving = false;
        }
    }

    public void MoveInDirection(Vector3 direction)
    {
        if (isMoving) return;

        Vector3 desiredPos = rb.position + direction.normalized * moveStep;

        // ✅ Çarpışma kontrolü (BoxCast)
        Vector3 halfExtents = col.bounds.extents;
        RaycastHit hit;

        bool blocked = Physics.BoxCast(rb.position, halfExtents * 0.9f, direction.normalized, out hit, transform.rotation, moveStep);

        if (!blocked)
        {
            targetPosition = desiredPos;
            isMoving = true;
        }
        else
        {
            Debug.Log("Hedefte çarpışma var: " + hit.collider.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(targetPosition, GetComponent<Collider>().bounds.size * 0.95f);
    }
}
