using UnityEngine;

public class CharacterMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f; // ��melme h�z�
    public float jumpForce = 10f;
    public float gravity = 20f;
    private float originalHeight;
    public float crouchHeight = 1f; // ��melince boy

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height; // Oyuncunun orijinal y�ksekli�ini kaydet
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Sa�a-Sola hareket
        moveDirection.x = moveInput * (isCrouching ? crouchSpeed : moveSpeed); // E�er ��meliyorsa h�z d��er

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && !isCrouching) // ��melirken z�playamaz
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime; // Yer�ekimi
        }

        moveDirection.z = 0; // Z ekseninde hareketi engelle

        controller.Move(moveDirection * Time.deltaTime);

        HandleCrouch(); // ��melme fonksiyonunu �a��r
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) // "CTRL" tu�una bas�nca ��mel
        {
            isCrouching = true;
            controller.height = crouchHeight; // Boyu k���lt
            Debug.Log("Oyuncu ��meldi!");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) // "CTRL" b�rak�l�nca normale d�n
        {
            isCrouching = false;
            controller.height = originalHeight; // Boyu eski haline getir
            Debug.Log("Oyuncu ��melmeyi b�rakt�!");
        }
    }
}
