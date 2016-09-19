using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public float maxSpeed = 20f;

    private Vector2 direction = Vector2.right;

    private Animator animator;

    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        var movement = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(movement * maxSpeed, rigidBody.velocity.y);

        if(movement != 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                animator.Play("Run");
            }
        } else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.Play("Idle");
            }
        }

        if(movement > 0)
        {
            direction = Vector2.right;
        }
        if (movement < 0)
        {
            direction = Vector2.left;
        }
        transform.localScale = new Vector2(direction.x, transform.localScale.y);
    }
}
