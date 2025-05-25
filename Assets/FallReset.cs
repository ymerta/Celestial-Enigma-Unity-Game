using UnityEngine;
using UnityEngine.SceneManagement;

public class FallReset : MonoBehaviour
{
    public float resetYThreshold = -10f;         // Ne kadar a�a�� d��erse reset?
    public Transform respawnPoint;               // Geri d�nece�i nokta
    public GameObject player;                    // Oyuncu objesi

    void Update()
    {
        if (player.transform.position.y < resetYThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false; // yeniden konumland�rmadan �nce kapat
            player.transform.position = respawnPoint.position;
            cc.enabled = true;
        }
        else
        {
            player.transform.position = respawnPoint.position;
        }
    }
}
