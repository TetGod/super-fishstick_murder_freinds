using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour

{
    public bool isdead;
    public int moveDir;
    public float moveSpeed;
    public float fallSpeed;

    private Rigidbody2D rig;

    public bool isActive;

    public GameObject player;
    public Player_Controller player_script;
    public Animator animator;

    public LayerMask layer;
    public bool canFall;
    public Vector3 startPos;

    public float countTime;
    public float timer;
    public float jumpForce;





    // Start is called before the first frame update
    void Start()
    {
        isdead = false;
        isActive = false;
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        player_script = player.GetComponent<Player_Controller>();
        moveDir = -1;
        canFall = true;
        startPos = transform.position;
        timer = countTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            animator.SetBool("isdead", isdead);
            move();
            timer -= Time.deltaTime;

        }
    }

    public void die()
    {
        isdead = true;
        Destroy(gameObject, 1f);
    }

    private void move()
    {
        //transform.localScale = new Vector3(moveDir * -1, 1, 1);
        if (timer < 0)
        {
            rig.AddForce(new Vector3(moveSpeed * moveDir * jumpForce, moveSpeed * jumpForce, 0), ForceMode2D.Impulse);
            moveDir *= -1;
            timer = countTime;
            transform.localScale = new Vector3(moveDir * -1, 1, 1);
        } 
        
    }

    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.transform.position.y < transform.position.y)
            {
                player_script.die();
            } 
            else
            {
                die();
            }

        }
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canFall)
        {
            canFall = false;
            rig.AddForce(Vector3.down * fallSpeed, ForceMode2D.Impulse);

        } 
    }
}
