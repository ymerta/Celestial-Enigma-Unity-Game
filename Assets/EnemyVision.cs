using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f; // G�r�� mesafesi
    public float chaseSpeed = 3f; // Takip h�z�
    private bool isChasing = false;
    private PlayerStealth playerStealth; // Oyuncunun saklan�p saklanmad���n� kontrol etmek i�in

    void Start()
    {
        playerStealth = player.GetComponent<PlayerStealth>(); // Oyuncunun saklanma durumunu al
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // **E�er oyuncu sakland�ysa, NPC onu g�rmeyecek**
        if (distanceToPlayer < detectionRange && playerStealth != null && !playerStealth.IsHidden())
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }
}
