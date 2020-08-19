using UnityEngine;
using System.Collections;

public class AliceController : MonoBehaviour {
    public float movePower = 1f;
    public float jumpPower = 1f;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer renderer;
    

    Vector3 movement;
    bool isJumping = false;

    //----------[Override Function]
    //Initailization
    void Start(){
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    //Graphic & Input Updates
    void Update(){
        //Moving
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isMoving", true);
            renderer.flipX = true;
            
            
        }else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isMoving", true);
            renderer.flipX = false;
        }

        //Jumping
        if(Input.GetButtonDown ("Jump") && !animator.GetBool("isJumping")){
            isJumping = true;
            animator.SetBool("isJumping", true); //Jumping Flag
            animator.SetTrigger("doJumping"); //Jumping Animation
        }
    }

    //Physics engine Updates
    void FixedUpdate(){
        Move ();
        Jump ();
    }

    //------[Movement Function]

    void Move(){
        Vector3 moveVelocity= Vector3.zero;

        if(Input.GetAxisRaw ("Horizontal") < 0){
            moveVelocity = Vector3.left;
            

        }else if(Input.GetAxisRaw("Horizontal") > 0){
            moveVelocity = Vector3.right;

        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump(){
        if(!isJumping)
            return;

        //Prevent Velocity amplifivation.
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2 (0, jumpPower);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    
    //Attach Event
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attach : " + other.gameObject.layer);

        if (other.gameObject.layer == 8)
            animator.SetBool("isJumping", false); //Landing
    }

    //Detach Event
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Detach : " + other.gameObject.layer);
    }
}