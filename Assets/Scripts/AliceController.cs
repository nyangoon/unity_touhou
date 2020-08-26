using UnityEngine;
using System.Collections;

public class AliceController : MonoBehaviour {
    public float movePower = 1f;
    public float dashPower = 1f;
    public float jumpPower = 1f;

    Rigidbody2D rigid;
    Animator animator;
    new SpriteRenderer renderer;


    Vector3 movement;
    bool isJumping = false;
    bool isDash = false;

    //----------[Override Function]
    //Initailization
    void Start() {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    //Graphic & Input Updates
    void Update() {
        //Moving
        if (Input.GetAxisRaw("Horizontal") == 0) //방향키 입력 없음
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isDash = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isDash = false;
            }
            animator.SetBool("isMoving", false);
            animator.SetBool("isDash", false);
           
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("isDash", true);
                isDash = true;
            }else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("isDash", false);
                isDash = false;
            }
            renderer.flipX = true;
            animator.SetBool("isMoving", true);
           
            
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("isDash", true);
                isDash = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("isDash", false);
                isDash = false;
            }
            renderer.flipX = false;
            animator.SetBool("isMoving", true);
            

        }
        

        //Jumping
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping")) {
            isJumping = true;
            animator.SetBool("isJumping", true); //Jumping Flag
            animator.SetTrigger("doJumping"); //Jumping Animation
        }
    }

    //Physics engine Updates
    void FixedUpdate() {
        Move();
        Jump();
    }

    //------[Movement Function]

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;

        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;

        }
        if (isDash == false)
        {
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }else if (isDash == true)
        {
            transform.position += moveVelocity * dashPower * Time.deltaTime;
        }
    
    }

    
    void Jump() {
        if (!isJumping)
            return;

        //Prevent Velocity amplifivation.
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    
    
    //Attach Event
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attach : " + other.gameObject.layer);

        if (other.gameObject.layer == 8 && rigid.velocity.y < 0)
            Debug.Log("land");
            animator.SetBool("isJumping", false); //Landing
    }

    //Detach Event
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Detach : " + other.gameObject.layer);
    }
}