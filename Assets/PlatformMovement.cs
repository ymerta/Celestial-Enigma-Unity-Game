using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 1f; // Bekleme süresi

    private Transform currentTarget;
    private bool isWaiting = false;

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        if (isWaiting) return;

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            StartCoroutine(WaitAndSwitchTarget());
        }
    }

    IEnumerator WaitAndSwitchTarget()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentTarget = currentTarget == pointA ? pointB : pointA;
        isWaiting = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }

    void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }

    // (Ýsteðe baðlý) editörde yön çiz
    void OnDrawGizmos()
    {
        if (pointA && pointB)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawSphere(pointA.position, 0.1f);
            Gizmos.DrawSphere(pointB.position, 0.1f);
        }
    }
}
