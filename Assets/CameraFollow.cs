using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    [Header("Kamera Ofsetleri")]
    public Vector3 sideScrollOffset = new Vector3(0, 2, -10);  // 2.5D offset
    public Vector3 isometricOffset = new Vector3(0, 8, -6);    // �zometrik offset

    [Header("Kamera A��lar�")]
    public Vector3 sideScrollRotationEuler = new Vector3(0, 0, 0);
    public Vector3 isometricRotationEuler = new Vector3(30, 45, 0);

    [Header("Takip Ayarlar�")]
    public float smoothSpeed = 0.125f;

    [Header("Ge�erli Mod")]
    public bool isIsometric = false;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 offset = isIsometric ? isometricOffset : sideScrollOffset;
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        Quaternion targetRotation = Quaternion.Euler(
            isIsometric ? isometricRotationEuler : sideScrollRotationEuler
        );
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
    }

    public void SwitchToIsometric()
    {
        isIsometric = true;
    }

    public void SwitchToSideScroll()
    {
        isIsometric = false;
    }

    public void SnapToTarget()
    {
        Vector3 offset = isIsometric ? isometricOffset : sideScrollOffset;
        transform.position = player.position + offset;

        transform.rotation = Quaternion.Euler(
            isIsometric ? isometricRotationEuler : sideScrollRotationEuler
        );
    }
}
