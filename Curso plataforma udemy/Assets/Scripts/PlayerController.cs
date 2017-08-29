using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidBody;
    private SpriteRenderer myRenderer;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    private Animator myAnim;

	// Use this for initialization  
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();//Associa o Sprite Renderer do objeto á variavel myRenderer, no comeco do jogo
    }   
	
	// Update is called once per frame
	void Update () {

        isGrounded = Physics2D.OverlapCircle
            ( groundCheck.position,
             groundCheckRadius,
               whatIsGround);
           


		if(Input.GetAxisRaw("Horizontal") > 0f)
        {
            myRigidBody.velocity = new Vector3(moveSpeed, myRigidBody.velocity.y, 0f);
            myRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
            myRenderer.flipX = true;
        }
        else
        {
            myRigidBody.velocity = new Vector3(0, myRigidBody.velocity.y, 0f);

        }
        if (Input.GetButtonDown("Jump") && isGrounded==true)
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpSpeed, 0f);

        }
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);
	}
}
