using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform camera;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float speed = 12f;

    private Vector3 velocity;
    private Animator animator;
    private AudioSource audioSource;

    private bool isGrounded;
    private float turnSmootVelocity;
    private float gravity = -9.81f;
    private float jumpHeight = 3f;
    private float groundDistance = 0.4f;
    private float turnSmoothTime = 0.1f;

    public AudioClip jump;
    public AudioClip land;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        StopAnimations();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameRunning()) {
            // Check if player is standing on the floor
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                StopJump();
            }

            // Basic player movement
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(x, 0, z);

            if (direction.magnitude >= 0.01f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmootVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(moveDirection * speed * Time.deltaTime);
                if (isGrounded)
                {
                    Walk();
                }
            }
            else
            {
                StopWalk();
            }

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                Jump();
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void StopAnimations()
    {
        animator.SetBool(Constants.IS_WALKING, false);
        animator.SetBool(Constants.IS_JUMPING, false);
        audioSource.Stop();
    }

    private void Walk()
    {
        animator.SetBool(Constants.IS_WALKING, true);
        animator.SetBool(Constants.IS_JUMPING, false);
    }

    private void StopWalk()
    {
        animator.SetBool(Constants.IS_WALKING, false);
        audioSource.Stop();
    }

    private void Jump()
    {
        animator.SetBool(Constants.IS_WALKING, false);
        animator.SetBool(Constants.IS_JUMPING, true);
        audioSource.clip = jump;
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
        
    }

    private void StopJump()
    {
        animator.SetBool(Constants.IS_JUMPING, false);
        audioSource.clip = land;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
