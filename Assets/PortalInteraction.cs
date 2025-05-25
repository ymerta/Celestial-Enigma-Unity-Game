using UnityEngine;
using UnityEngine.SceneManagement; // �ste�e ba�l�

public class PortalInteraction : MonoBehaviour
{
    public Transform teleportTarget; // I��nlanaca�� konum (istersen)
    public string portalName;        // �rn: "K�rm�z� Portal"

    private bool isPlayerInRange = false;
    private GameObject player;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Portala girildi: " + portalName);

            // I��nlanmak istersen:
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

            // Veya sahne y�klemek istersen:
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
