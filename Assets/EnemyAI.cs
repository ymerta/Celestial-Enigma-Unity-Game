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

    [Header("Freeze Icon")]
    public GameObject freezeIconObject;

    // 🔹 STUN
    private bool isStunned = false;
    private float stunEndTime;

    void Start()
    {
        target = pointA.position;

        if (freezeIconObject != null)
            freezeIconObject.SetActive(false);
    }

    void Update()
    {
        // 🔒 Eğer stunlıysa, sadece süreyi kontrol et
        if (isStunned)
        {
            if (Time.time > stunEndTime)
            {
                Unstun();
            }
            return;
        }

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

        if (distanceToPlayer < detectionRange &&
            player.gameObject.layer != LayerMask.NameToLayer("Hidden") &&
            !playerStealth.IsHidden())
        {
            playerDetected = true;
            Debug.Log("Oyuncu tespit edildi! Takip başlıyor...");
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // 🔄 Oyuncuya bak
            Vector3 lookPos = player.position - transform.position;
            lookPos.y = 0; // Sadece yatay düzlemde döndür
            if (lookPos != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            }
        }
    }

    // 🔹 Stun uygulanınca çağrılacak
    public void Stun(float duration)
    {
        isStunned = true;
        stunEndTime = Time.time + duration;

        if (freezeIconObject != null)
            freezeIconObject.SetActive(true);

        Debug.Log($"{gameObject.name} sersemletildi!");
    }

    private void Unstun()
    {
        isStunned = false;

        if (freezeIconObject != null)
            freezeIconObject.SetActive(false);

        Debug.Log($"{gameObject.name} sersemletmeden çıktı.");
    }
}
