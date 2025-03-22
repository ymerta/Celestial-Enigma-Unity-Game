using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    private Renderer playerRenderer;
    private CharacterMovement3D movementScript;
    private Collider playerCollider;
    private bool isHidden = false;
    private bool canExit = false;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        movementScript = GetComponent<CharacterMovement3D>();
        playerCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (isHidden && canExit && Input.GetKeyDown(KeyCode.E))
        {
            ExitHiding();
        }
    }

    public void EnterHiding()
    {
        isHidden = true;
        canExit = true;
        playerRenderer.enabled = false; // Oyuncuyu g�r�nmez yap
        movementScript.enabled = false; // Hareketi kapat

        // **NPC'nin oyuncuyla �arp��mas�n� kapat**
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        Debug.Log("Oyuncu G�ZLEND�! 'E' tu�una basarak ��kabilirsin.");
    }

    public void ExitHiding()
    {
        isHidden = false;
        canExit = false;
        playerRenderer.enabled = true; // Oyuncuyu tekrar g�r�n�r yap
        movementScript.enabled = true; // Hareketi tekrar a�

        // **NPC'nin oyuncuyla �arp��mas�n� tekrar a�**
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        Debug.Log("Oyuncu �IKTI ve tekrar g�r�nd�!");
    }

    public bool IsHidden()
    {
        return isHidden;
    }
}
