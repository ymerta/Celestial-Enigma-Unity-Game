using UnityEngine;

public class CharacterMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f; // Çömelme hýzý
    public float jumpForce = 10f;
    public float gravity = 20f;
    private float originalHeight;
    public float crouchHeight = 1f; // Çömelince boy

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isCrouching = false;

    public bool isIsometric = false; // Ýzometrik kontrol aktif mi?

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height; // Oyuncunun orijinal yüksekliðini kaydet
    }

    void Update()
    {
        if (isIsometric)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 input = new Vector3(moveX, 0, moveZ).normalized;

            // Kamera yönüne göre input'u döndür
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            // Y düzleminde düzleþtir
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = camForward * input.z + camRight * input.x;
            move = move.normalized * (isCrouching ? crouchSpeed : moveSpeed);

            moveDirection.x = move.x;
            moveDirection.z = move.z;
        }

        else
        {
            // 2.5D mod: sadece X ekseninde hareket
            float moveInput = Input.GetAxis("Horizontal");
            moveDirection.x = moveInput * (isCrouching ? crouchSpeed : moveSpeed);
            moveDirection.z = 0; // Z ekseni kilitli
        }

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && !isCrouching) // Çömelirken zýplayamaz
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime; // Yerçekimi
        }

        controller.Move(moveDirection * Time.deltaTime);

        HandleCrouch(); // Çömelme fonksiyonunu çaðýr
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) // "CTRL" tuþuna basýnca çömel
        {
            isCrouching = true;
            controller.height = crouchHeight; // Boyu küçült
            Debug.Log("Oyuncu çömeldi!");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) // "CTRL" býrakýlýnca normale dön
        {
            isCrouching = false;
            controller.height = originalHeight; // Boyu eski haline getir
            Debug.Log("Oyuncu çömelmeyi býraktý!");
        }
    }

    public void TeleportTo(Vector3 newPosition)
    {
        controller.enabled = false;
        transform.position = newPosition;
        controller.enabled = true;


        // Kamera deðiþtirme
        FindFirstObjectByType<CameraSwitch>().SwitchToIsometric();
    }
}
