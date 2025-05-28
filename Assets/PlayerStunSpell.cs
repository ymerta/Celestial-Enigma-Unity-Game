using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerStunSpell : MonoBehaviour
{
    public float stunRange = 5f;            // Büyünün etki alanı
    public float stunDuration = 3f;         // Stun süresi
    public float cooldown = 5f;             // Büyü tekrar kullanılabilir olana kadar geçen süre

    private float lastUsedTime;

    [Header("UI Bileşenleri")]
    public Image stunIcon;                  // UI'daki stun büyüsü ikonu
    public TMP_Text stunCooldownText;       // Geri sayım yazısı

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastUsedTime + cooldown)
        {
            CastStun();
            lastUsedTime = Time.time;

            // UI'de cooldown göstergesi başlat
            StartCoroutine(SetStunIconCooldown(cooldown));
        }
    }

    void CastStun()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, stunRange);

        foreach (Collider hit in hits)
        {
            EnemyAI enemy = hit.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Stun(stunDuration);
            }
        }

        Debug.Log("Stun büyüsü kullanıldı!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stunRange);
    }

    IEnumerator SetStunIconCooldown(float duration)
    {
        if (stunIcon != null)
            stunIcon.color = new Color(0.3f, 0.3f, 0.3f, 1f); // Karart

        if (stunCooldownText != null)
            stunCooldownText.gameObject.SetActive(true);

        float remaining = duration;

        while (remaining > 0)
        {
            if (stunCooldownText != null)
                stunCooldownText.text = Mathf.CeilToInt(remaining).ToString();

            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }

        if (stunIcon != null)
            stunIcon.color = Color.white; // Geri beyazlat

        if (stunCooldownText != null)
            stunCooldownText.gameObject.SetActive(false);
    }
}
