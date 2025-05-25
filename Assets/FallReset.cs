using UnityEngine;
using UnityEngine.SceneManagement;

public class FallReset : MonoBehaviour
{
    public float resetYThreshold = -10f;         // Ne kadar aþaðý düþerse reset?
    public Transform respawnPoint;               // Geri döneceði nokta
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
            cc.enabled = false; // yeniden konumlandýrmadan önce kapat
            player.transform.position = respawnPoint.position;
            cc.enabled = true;
        }
        else
        {
            player.transform.position = respawnPoint.position;
        }
    }
}
