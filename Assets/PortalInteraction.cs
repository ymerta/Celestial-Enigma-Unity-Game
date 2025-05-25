using UnityEngine;
using UnityEngine.SceneManagement; // Ýsteðe baðlý

public class PortalInteraction : MonoBehaviour
{
    public Transform teleportTarget; // Iþýnlanacaðý konum (istersen)
    public string portalName;        // Örn: "Kýrmýzý Portal"

    private bool isPlayerInRange = false;
    private GameObject player;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Portala girildi: " + portalName);

            // Iþýnlanmak istersen:
            if (teleportTarget != null)
            {
                CharacterController cc = player.GetComponent<CharacterController>();
                if (cc != null)
                {
                    cc.enabled = false;
                    player.transform.position = teleportTarget.position;
                    cc.enabled = true;
                }
                else
                {
                    player.transform.position = teleportTarget.position;
                }
            }

            // Veya sahne yüklemek istersen:
            // SceneManager.LoadScene("MazeRealmScene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
