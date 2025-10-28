using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //control variables
    public float groundCheckRadius = 0.02f;
    public float speed = 10f;
    public bool isGrounded = false;
    private Vector2 groundCheckPos => new Vector2(col.bounds.center.x, col.bounds.min.y);
    public bool isFire = false;
    public bool airFire = false;

    //layer mask to identify what is ground
    private LayerMask groundLayer;

    //component refrences
    //public Transform groundCheck;
    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer sr;
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        groundLayer = LayerMask.GetMask("Ground");

        //initialize ground check position using separate game objec as a child fo the player
        //GameObject newObj = new GameObject("GroundCheck");
        //newObj.transform.SetParent(transform);
        //newObj.transform.localPosition = new Vector3.zero;
        //groundCheck = newObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);

        float hValue = Input.GetAxis("Horizontal");
        float vValue = Input.GetAxis("Vertical");
        SpriteFlip(hValue);

        rb.linearVelocityX = hValue * speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }

        anim.SetFloat("hvalue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFire", isFire);
        anim.SetBool("airFire", airFire);

        if (Input.GetButtonDown("Fire1"))
        {
            isFire = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isFire = false;
        }
       
        if (Input.GetButtonDown("Vertical") && !isGrounded)
        {
            airFire = true;
        }

        if (Input.GetButtonUp("Vertical") && isGrounded)
        {
            airFire = false; 
        }
    }

    private void SpriteFlip(float hValue)
    {
        if (hValue != 0)
        {
            sr.flipX = (hValue < 0);
        }


        /* if (sr.flipX && hValue > 0 || !sr.flipX && hValue < 0)
         {
             sr.flipX = !sr.flipX;
         }
        */
    }
}
