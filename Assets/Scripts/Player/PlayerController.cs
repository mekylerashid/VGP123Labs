using System.Collections;
using UnityEngine;
[RequireComponent (typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    #region Control Vars
    //control variables
    public float groundCheckRadius = 0.02f;
    public float speed = 10f;
    public float jumpForce = 10f;
    public bool isGrounded = false;  
   // public int maxLives = 10;
    //private int _lives = 5;
    #endregion
    #region Set/Get Lives
    //public int lives
    //{
    //    get => _lives;
    //    set
    //        {
    //        if (value < 0)
    //        GameOver();
    //        if (value > maxLives)
    //        {
    //            _lives = maxLives;
              
    //        }
    //        else
    //        {
    //            _lives = value;
    //        }

    //        Debug.Log($"Life value has changed to {_lives}");
    //    }
    //}
    #endregion
    //private void GameOver()
    //{
    //    Debug.Log("Game Over!");
    //}
  
    //private Vector2 groundCheckPos => new Vector2(col.bounds.center.x, col.bounds.min.y);
    public bool isFire = false;
    public bool airFire = false;
    public float initialPowerupTimer = 5f;

    //layer mask to identify what is ground
    //private LayerMask groundLayer;

    #region Component Ref
    //component refrences
    //public Transform groundCheck;
    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer sr;
    Animator anim;
    GroundCheck groundCheck;
    #endregion

    //State Variables
    Coroutine jumpForceCoroutine;
    float jumpPowerupTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //groundLayer = LayerMask.GetMask("Ground");

        groundCheck = new GroundCheck(col, LayerMask.GetMask("Ground"), groundCheckRadius);

        //initialize ground check position using separate game objec as a child fo the player
        //GameObject newObj = new GameObject("GroundCheck");
        //newObj.transform.SetParent(transform);
        //newObj.transform.localPosition = new Vector3.zero;
        //groundCheck = newObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        isGrounded = groundCheck.CheckIsGrounded();


        float hValue = Input.GetAxis("Horizontal");
        float vValue = Input.GetAxis("Vertical");
        SpriteFlip(hValue);

        rb.linearVelocityX = hValue * speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Fire");
        }

        anim.SetFloat("hvalue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFire", isFire);
        anim.SetBool("airFire", airFire);

        #region Lab Attack
        /*if (Input.GetButtonDown("Fire1"))
        {
            isFire = true;
        }

        //if (Input.GetButtonUp("Fire1"))
        {
            isFire = false;
        }*/
        #endregion

        if (Input.GetButtonDown("Vertical") && !isGrounded)
        {
            airFire = true;
        }

        if (Input.GetButtonUp("Vertical") && isGrounded)
        {
            airFire = false; 
        }
    }

    private void OnValidate() => groundCheck?.UpdateGroundCheckRadius(groundCheckRadius);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage(1);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) return;

        Debug.Log("Collided with: " + collision.gameObject.name);
    }*/

    //These functions are called when a trigger collider is entered, stayed in, or exited - they don't really have any limits on what they can interact with
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Squish") && rb.linearVelocityY < 0)
        {
            collision.GetComponentInParent<BaseEnemy>().TakeDamage(0, DamageType.JumpedOn);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //if (collision.gameObject.CompareTag("Range"))
        //{
          //  Debug.Log("Within Range");
            //collision.GetComponent<TurretEnemy>().anim.SetTrigger("Fire");
            //collision.GetComponent<TurretEnemy>().sr.flipX = !sr.flipX;
        //}
        //Destroy(collision.gameObject);
    }
    public void TakeDamage(int livesLost)
    {
        GameManager.Instance.lives -= livesLost;
    }

    public void IncreaseGravity() => rb.gravityScale = 5f;
    public void ApplyJumpForcePowerup()
    {
        if (jumpForceCoroutine  != null)
        {
            StopCoroutine(jumpForceCoroutine);
            jumpForceCoroutine = null;
            jumpForce = 7;
        }
        jumpForceCoroutine = StartCoroutine(JumpForceChange());
    }
    IEnumerator JumpForceChange()
    {
        jumpPowerupTimer = initialPowerupTimer + jumpPowerupTimer;
        jumpForce = 10f;

        while (jumpPowerupTimer>0)
        {
            jumpPowerupTimer -= Time.deltaTime;
            Debug.Log("Jump Powerup Time: " + jumpPowerupTimer);
            yield return null;
        }

        jumpForce = 7;
        jumpForceCoroutine = null;
        jumpPowerupTimer = 0;
    }
}

