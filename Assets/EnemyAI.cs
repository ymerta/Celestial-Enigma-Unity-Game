using NUnit.Framework.Internal;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool test;
    public Transform player;
    public float detectionRange = 5f;
    public bool playerDetected = false;
    public Transform pointA, pointB;
    public float moveSpeed = 2f;
    private Vector3 target;
    private bool stopMoving = false;

    void Start()
    {
        target = pointA.position;
    }

    void Update()
    {
        if (!stopMoving)
        {
            Patrol();
            DetectPlayer();
        }
    }

    void Patrol()
    {
        if (!playerDetected)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, pointA.position) < 0.2f)
            {
                target = pointB.position;
                transform.Rotate(0, 180, 0);
            }
            else if (Vector3.Distance(transform.position, pointB.position) < 0.2f)
            {
                target = pointA.position;
                transform.Rotate(0, 180, 0);
            }
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        PlayerStealth playerStealth = player.GetComponent<PlayerStealth>();

        // **Eðer oyuncu "Hidden" layer'ýndaysa veya saklandýysa, NPC onu görmeyecek**
        if (distanceToPlayer < detectionRange && player.gameObject.layer != LayerMask.NameToLayer("Hidden") && !playerStealth.IsHidden())
        {
            playerDetected = true;
            Debug.Log("Oyuncu tespit edildi! Takip baþlýyor...");
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
