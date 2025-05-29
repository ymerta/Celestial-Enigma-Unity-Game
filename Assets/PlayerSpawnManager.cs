using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public static Vector3? customSpawnPoint = null; // Nullable vektör

    public static void SetSpawnPoint(Vector3 spawnPoint)
    {
        customSpawnPoint = spawnPoint;
    }

    public static Vector3 GetSpawnPoint(Vector3 defaultSpawn)
    {
        return customSpawnPoint.HasValue ? customSpawnPoint.Value : defaultSpawn;
    }
}
