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
            alreadyTriggered = true; // bir kere tetiklenmesini sa�la
            StartCoroutine(TransitionToIsometric());
        }
    }

    IEnumerator TransitionToIsometric()
    {
        if (cameraFollow == null || movementScript == null || isometricSpawnPoint == null || fadeController == null)
        {
            Debug.LogError("Eksik referans var! L�tfen Inspector'dan t�m alanlar� doldurun.");
            yield break;
        }

        yield return fadeController.FadeOut();

        // Oyuncuyu ���nla
        movementScript.TeleportTo(isometricSpawnPoint.position);

        // Kamera ge�i�i
        cameraFollow.SwitchToIsometric();
        cameraFollow.SnapToTarget();

        // Hareket modunu de�i�tir
        movementScript.isIsometric = true;

        yield return fadeController.FadeIn();

        Debug.Log("? Oyuncu izometrik moda ge�ti ve ge�i� efekti ba�ar�yla tamamland�.");
    }
}
