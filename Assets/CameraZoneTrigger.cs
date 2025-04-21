using UnityEngine;
using System.Collections;

public class CameraZoneTrigger : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public CharacterMovement3D movementScript;
    public Transform isometricSpawnPoint;
    public FadeController fadeController; // ?? Yeni eklendi

    private bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyTriggered)
        {
            alreadyTriggered = true; // bir kere tetiklenmesini saðla
            StartCoroutine(TransitionToIsometric());
        }
    }

    IEnumerator TransitionToIsometric()
    {
        if (cameraFollow == null || movementScript == null || isometricSpawnPoint == null || fadeController == null)
        {
            Debug.LogError("Eksik referans var! Lütfen Inspector'dan tüm alanlarý doldurun.");
            yield break;
        }

        yield return fadeController.FadeOut();

        // Oyuncuyu ýþýnla
        movementScript.TeleportTo(isometricSpawnPoint.position);

        // Kamera geçiþi
        cameraFollow.SwitchToIsometric();
        cameraFollow.SnapToTarget();

        // Hareket modunu deðiþtir
        movementScript.isIsometric = true;

        yield return fadeController.FadeIn();

        Debug.Log("? Oyuncu izometrik moda geçti ve geçiþ efekti baþarýyla tamamlandý.");
    }
}
