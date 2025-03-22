using UnityEngine;

public class HideZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu saklandý! 'E' tuþuna basarak çýkabilirsin.");
            PlayerStealth stealth = other.GetComponent<PlayerStealth>();
            if (stealth != null)
            {
                stealth.EnterHiding();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu saklanmayý býraktý!");
            PlayerStealth stealth = other.GetComponent<PlayerStealth>();
            if (stealth != null)
            {
                stealth.ExitHiding();

                // **NPC'ye oyuncunun artýk saklanmadýðýný bildiriyoruz!**
                EnemyAI enemyAI = FindFirstObjectByType<EnemyAI>();
                if (enemyAI != null)
                {
                    Debug.Log("NPC artýk oyuncuyu tekrar fark edebilir.");
                }
            }
        }
    }
}
