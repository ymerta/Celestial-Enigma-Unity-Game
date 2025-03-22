using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Oyuncunun transformu
    public Vector3 offset = new Vector3(0, 2, -10);  // Kamera ile oyuncu aras�ndaki mesafe
    public float smoothSpeed = 0.125f;  // Takip h�z�n� ayarla

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
