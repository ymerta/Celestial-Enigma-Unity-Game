using UnityEngine;
using UnityEngine.UI;

public class SpellHUD : MonoBehaviour
{
    public Image slot1;
    public Image slot2;
    public Image slot3;

    public Sprite lightOrbIcon;
    public Sprite stunIcon;
    public Sprite platformIcon;

    public void LearnSpell(int spellIndex)
    {
        switch (spellIndex)
        {
            case 0:
                slot1.sprite = lightOrbIcon;
                slot1.gameObject.SetActive(true);
                break;
            case 1:
                slot2.sprite = stunIcon;
                slot2.gameObject.SetActive(true);
                break;
            case 2:
                slot3.sprite = platformIcon;
                slot3.gameObject.SetActive(true);
                break;
        }
    }
}
