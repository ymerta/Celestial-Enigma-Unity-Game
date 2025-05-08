using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineCamera sideScrollVCam;
    public CinemachineCamera isometricVCam;
    public CharacterMovement3D playerMovement;  // Player movement script

    public void SwitchToIsometric()
    {
        sideScrollVCam.Priority = 0;
        isometricVCam.Priority = 10;

        playerMovement.isIsometric = true; // HAREKET sistemi de izometrik olacak
    }

    public void SwitchToSideScroll()
    {
        sideScrollVCam.Priority = 10;
        isometricVCam.Priority = 0;

        playerMovement.isIsometric = false; // HAREKET sistemi 2.5D olacak
    }
}
