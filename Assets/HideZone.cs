using UnityEngine;

public class HideZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu sakland�! 'E' tu�una basarak ��kabilirsin.");
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
            Debug.Log("Oyuncu saklanmay� b�rakt�!");
            PlayerStealth stealth = other.GetComponent<PlayerStealth>();
            if (stealth != null)
            {
                stealth.ExitHiding();

                // **NPC'ye oyuncunun art�k saklanmad���n� bildiriyoruz!**
                EnemyAI enemyAI = FindFirstObjectByType<EnemyAI>();
                if (enemyAI != null)
                {
                    Debug.Log("NPC art�k oyuncuyu tekrar fark edebilir.");
                }
            }
        }
    }
}
