using UnityEngine;

public class PlayerFinishGame : MonoBehaviour
{
    public GameObject endGameCanvas;  // Inspector�dan Canvas atanacak

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hat"))  // "Hat" tag'li objeye �arpt�ysa
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (endGameCanvas != null)
        {
            endGameCanvas.SetActive(true); // Paneli g�ster
        }

        Time.timeScale = 0f; // Oyunu durdur
        Cursor.lockState = CursorLockMode.None; // Fareyi serbest b�rak
        Cursor.visible = true;
    }
}
