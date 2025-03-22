using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f; // Görüþ mesafesi
    public float chaseSpeed = 3f; // Takip hýzý
    private bool isChasing = false;
    private PlayerStealth playerStealth; // Oyuncunun saklanýp saklanmadýðýný kontrol etmek için

    void Start()
    {
        playerStealth = player.GetComponent<PlayerStealth>(); // Oyuncunun saklanma durumunu al
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // **Eðer oyuncu saklandýysa, NPC onu görmeyecek**
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
