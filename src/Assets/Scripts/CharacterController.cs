using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float maxSpeed = 20f;

    private float movement;

    private Vector2 direction = Vector2.right;

    private Animator animator;

    private Rigidbody2D rigidBody;

    private bool isGrounded = false;

    public Transform groundCheck;

    public LayerMask whatIsGround;

    public int jumpForce = 10500;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //если персонаж на земле и нажат пробел...
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Grounded", false);
            rigidBody.AddForce(new Vector2(0, jumpForce));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = groundCheck.GetComponent<Collider2D>().IsTouchingLayers(whatIsGround);

        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("vSpeed", rigidBody.velocity.y);

        movement = Input.GetAxisRaw("Horizontal");
        if (isGrounded)
        {
            rigidBody.velocity = new Vector2(movement * maxSpeed, rigidBody.velocity.y);

        }
        else
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x * 0.5f + movement * maxSpeed * 0.3f, rigidBody.velocity.y);
        }

        if (isGrounded)
            ToggleRunAnimation(movement);

        ToggleDirection(movement);
    }

    private void ToggleDirection(float movement)
    {
        if (movement > 0)
        {
            direction = Vector2.right;
        }
        if (movement < 0)
        {
            direction = Vector2.left;
        }
        transform.localScale = new Vector2(direction.x, transform.localScale.y);
    }

    private void ToggleRunAnimation(float movement)
    {
        if (movement != 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                animator.Play("Run");
            }
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.Play("Idle");
            }
        }
    }
}
