using UnityEngine;

public class CharacterMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 10f;
    public float gravity = 20f;
    private float originalHeight;
    public float crouchHeight = 1f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isCrouching = false;
public bool isControllable = true; // dışarıdan kapatıp açmak için

    public bool isIsometric = false;

    private Animator animator;
    private float previousDirection = 1f; // Dönüş yönü kontrolü

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        animator = GetComponentInChildren<Animator>(); // Animator, "kiz" alt objesinde ise
    }

    void Update()
    {
        if (!isControllable) return;

        if (isIsometric)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 input = new Vector3(moveX, 0, moveZ).normalized;

            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = camForward * input.z + camRight * input.x;
            move = move.normalized * (isCrouching ? crouchSpeed : moveSpeed);

            moveDirection.x = move.x;
            moveDirection.z = move.z;

            // ✅ Karakterin yönünü X ekseninde çevir (sağa/sola dönsün)
            if (input.z != 0)
            {
                float currentDirection = Mathf.Sign(input.z);
                Vector3 scale = transform.localScale;
                scale.x = currentDirection * Mathf.Abs(scale.z);
                transform.localScale = scale;
            }
          

            bool isWalking = moveDirection.x != 0 || moveDirection.z != 0;
            animator.SetBool("isWalking", isWalking);
        }

        else
        {
            float moveInput = Input.GetAxis("Horizontal");
            moveDirection.x = moveInput * (isCrouching ? crouchSpeed : moveSpeed);
            moveDirection.z = 0;

            bool isWalking = Mathf.Abs(moveDirection.x) > 0.01f;
            animator.SetBool("isWalking", isWalking);

            // Dönüş kontrolü ve yön flip
            if (moveInput != 0)
            {
                float currentDirection = Mathf.Sign(moveInput);

                if (currentDirection != previousDirection)
                {
                    if (currentDirection > previousDirection)
                        animator.SetTrigger("turnRight");
                    else
                        animator.SetTrigger("turnLeft");

                    previousDirection = currentDirection;
                }

                // Karakterin yüzünü çevirmek
                Vector3 scale = transform.localScale;
                scale.x = currentDirection * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
        }

        if (controller.isGrounded)
        {
            animator.SetBool("isJumping", false);

            if (Input.GetButtonDown("Jump") && !isCrouching)
            {
                moveDirection.y = jumpForce;
                animator.SetBool("isJumping", true);
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);

        HandleCrouch();
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            controller.height = crouchHeight;
            animator.SetBool("isCrouching", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            controller.height = originalHeight;
            animator.SetBool("isCrouching", false);
        }
    }

    public void TeleportTo(Vector3 newPosition)
    {
        controller.enabled = false;
        transform.position = newPosition;
        controller.enabled = true;

        FindFirstObjectByType<CameraSwitch>().SwitchToIsometric();
    }
}
