using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{
    public float moveStep = 2f;      // Her adımda kaç birim hareket etsin
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                isMoving = false;
        }
    }

    public void MoveInDirection(Vector3 direction)
    {
        if (!isMoving)
        {
            targetPosition = transform.position + direction.normalized * moveStep;
            isMoving = true;
        }
    }
}
