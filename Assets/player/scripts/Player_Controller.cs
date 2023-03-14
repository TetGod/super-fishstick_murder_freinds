using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("health")]
    public int health;
    public int lives;
    [SerializeField]
    private bool isDead;


    [Header("Movment")]
    public float moveSpeed;
    public float x_input;
    public float y_input;
    public int facingDir;
    public bool isWalking;

    [Header("Jumping")]
    public float jumpForce;
    public int maxJumps;
    public int jumpcount;
    public bool isJumping;

    [Header("shooting")]
    public float shootSpeed;
    public GameObject bulletPrefab;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private Animator animator;
    public Game_Controller_Script gameController;

    [Header("Debug")]
    public bool debug;

    public int score;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI livesText;

    public Vector3 spawnPos;



    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingDir = 1;
        isDead = false;
        score = 0;
        lives = 3;
        spawnPos = GameObject.FindGameObjectWithTag("startpoint").transform.position;
        transform.position = spawnPos;
        gameController = FindObjectOfType<Game_Controller_Script>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        move();
    }

    // Update is called once per frame
    void Update()
    {
        // set animation state
        animator.SetBool("walking",isWalking);
        animator.SetBool("jumping", isJumping);
        animator.SetBool("die", isDead);


        // reset jumps if grounded
        if (rig.velocity.y <= 0)
        {
            if (isGrounded())
            {
                reset_Jumps();
            }
        }

        //check for jumps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }

    }

    // collision methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("damage"))
        {
            die();
        }
        if (collision.CompareTag("midpoint"))
        {
            spawnPos = collision.transform.position;
        }
        if (collision.CompareTag("Goal"))
        {
            gameController.next_level();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    // player actions
    private void move()
    {
        x_input = Input.GetAxis("Horizontal");
        if(x_input != 0 && !isJumping)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        if (x_input > 0)
        {
            facingDir = 1;
        }
        else if(x_input < 0)
        {
            facingDir = -1;
        }
        rig.velocity = new Vector2(x_input*moveSpeed,rig.velocity.y);
        transform.localScale= new Vector2(facingDir,transform.localScale.y);

    }
    private void jump()
    {
        if(jumpcount > 0)
        {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpcount--;
            isJumping = true;
        }

    }
    private void shoot()
    {

    }
    private void climb()
    {

    }
    private void duck()
    {

    }

    public void die()
    {
        isDead = true;
        lives -= 1;
        livesText.text = "Lives: " + lives.ToString();
        new WaitForSeconds(3);
        isDead = false;
        transform.position = spawnPos;
    }

    // utility functions
    private bool isGrounded()
    {
        RaycastHit2D hitC = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-.01f), Vector2.down, .05f);
        RaycastHit2D hitR = Physics2D.Raycast(new Vector2(transform.position.x+.25f, transform.position.y - .01f), Vector2.down, .05f);
        RaycastHit2D hitL = Physics2D.Raycast(new Vector2(transform.position.x-.25f, transform.position.y - .01f), Vector2.down, .05f);

        if (hitL )
        {
            if (hitL.collider.gameObject.CompareTag("Ground")){
                return true;
            }
        }
        if (hitR )
        {
            if (hitR.collider.gameObject.CompareTag("Ground"))
            {
                return true;
            }
        }
        if ( hitC)
        {
            if (hitC.collider.gameObject.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    private void reset_Jumps()
    {
        isJumping = false;
        jumpcount = maxJumps;
    }

    public void collect(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }

}
