using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerStunSpell : MonoBehaviour
{
    public float stunRange = 5f;
    public float stunDuration = 3f;
    public float cooldown = 5f;

    private float lastUsedTime;

    public bool hasStunSpell = false;

    [Header("UI Bileşenleri")]
    public Image stunIcon;
    public TMP_Text stunCooldownText;

    void Update()
    {
        // ✅ Sadece büyü öğrenildiyse R tuşu çalışsın
        if (hasStunSpell && Input.GetKeyDown(KeyCode.R) && Time.time >= lastUsedTime + cooldown)
        {
            CastStun();
            lastUsedTime = Time.time;
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
            stunIcon.color = Color.white;

        if (stunCooldownText != null)
            stunCooldownText.gameObject.SetActive(false);
    }
}
