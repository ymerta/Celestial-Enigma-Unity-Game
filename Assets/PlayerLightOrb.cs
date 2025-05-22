using UnityEngine;

public class PlayerLightOrb : MonoBehaviour
{
    public GameObject lightOrbPrefab;
    public float shootForce = 10f;
    public float cooldown = 5f;
    public float orbLifetime = 3f;

    private float lastUsedTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastUsedTime + cooldown)
        {
            ShootLightOrb();
            lastUsedTime = Time.time;
        }
    }

    void ShootLightOrb()
    {
        Vector3 spawnPos = transform.position + transform.forward * 0.5f + Vector3.up * 1f;
        GameObject orb = Instantiate(lightOrbPrefab, spawnPos, Quaternion.LookRotation(transform.forward));



        Rigidbody rb = orb.GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * shootForce;

        Destroy(orb, orbLifetime); // örn. 3 saniye sonra yok olur

    }
}
