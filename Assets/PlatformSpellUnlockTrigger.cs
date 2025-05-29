using UnityEngine;

public class PlatformSpellUnlockTrigger : MonoBehaviour
{
    public GameObject spellSlot3Image; // Canvas içindeki Image (SpellSlot3)

    private void Start()
    {
        if (spellSlot3Image != null)
            spellSlot3Image.SetActive(false); // Baþta görünmesin
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (spellSlot3Image != null)
                spellSlot3Image.SetActive(true); // Oyuncu girince aktif et
        }
    }
}
