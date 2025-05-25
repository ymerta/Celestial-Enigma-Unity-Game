using UnityEngine;

public class PlayerPlatformTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform.parent); // Parent = platform
    }

    void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
