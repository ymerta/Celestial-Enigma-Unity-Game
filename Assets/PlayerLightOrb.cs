using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI bileşenlerini kullanabilmek için
using System.Collections;
using TMPro; // TextMeshPro kullanımı için



public class PlayerLightOrb : MonoBehaviour
{
    [Header("Orb Ayarları")]
    public GameObject lightOrbPrefab;
    public float shootForce = 10f;
    public float cooldown = 3f;
    public float orbLifetime = 3f;
    public Image lightOrbIcon; // Inspector'dan atanacak
    private float lastUsedTime;
    private bool hasLearnedOrb = false;
    public TMP_Text orbCooldownText; // Inspector'dan atayacaksın

    void Start()
    {
        // Eğer bu MazeshiftRealm sahnesiyse, büyüyü otomatik öğrenmiş olarak başlasın
        if (SceneManager.GetActiveScene().name == "MazeShiftRealm")
        {
            LearnLightOrb();
        }
    }

    void Update()
    {
        // Büyü öğrenilmemişse hiçbir şey yapma
        if (!hasLearnedOrb)
            return;

        // Q tuşuna basıldığında ve cooldown geçtiğinde
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= lastUsedTime + cooldown)
        {
            ShootLightOrb();
            lastUsedTime = Time.time;
        }
    }
    IEnumerator SetOrbIconCooldown(float duration)
    {
        if (lightOrbIcon != null)
        {
            lightOrbIcon.color = new Color(0.3f, 0.3f, 0.3f, 1f); // Daha gri/siyaha yakın

        }

        if (orbCooldownText != null)
        {
            orbCooldownText.gameObject.SetActive(true);
        }

        float remaining = duration;

        while (remaining > 0)
        {
            if (orbCooldownText != null)
            {
                orbCooldownText.text = Mathf.CeilToInt(remaining).ToString(); // 3, 2, 1
            }

            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }

        if (lightOrbIcon != null)
        {
            lightOrbIcon.color = Color.white;
        }

        if (orbCooldownText != null)
        {
            orbCooldownText.gameObject.SetActive(false);
        }
    }


    public void LearnLightOrb()
    {
        hasLearnedOrb = true;
        Debug.Log("LightOrb büyüsü öğrenildi!");
    }

    public GameObject correctPortal; // Bunu Inspector'dan atayacaksın

    void ShootLightOrb()
    {
        Vector3 spawnPos = transform.position + Vector3.up * 1.2f;

        Vector3 direction;

        if (correctPortal != null)
        {
            direction = (correctPortal.transform.position - spawnPos).normalized;
        }
        else
        {
            // Oyuncunun sağa baktığı yön varsayılan olarak sağ (Vector3.right)
            direction = Vector3.right;

            // Eğer oyuncunun yönünü alabileceğin bir sistem varsa (örneğin scale ile bakış yönü), onu da kullanabilirsin:
            // direction = transform.right; // Bu da oyuncunun bakış yönü olur
        }

        GameObject orb = Instantiate(lightOrbPrefab, spawnPos, Quaternion.LookRotation(direction));

        Rigidbody rb = orb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * shootForce, ForceMode.VelocityChange);
        }
        StartCoroutine(SetOrbIconCooldown(cooldown));

        Destroy(orb, orbLifetime);
    }



}
