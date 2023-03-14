using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_Script : MonoBehaviour
{
    public bool isdead;
    public int moveDir;
    public float moveSpeed;

    private Rigidbody2D rig;

    public bool isActive;

    public GameObject player;
    public Player_Controller player_script;
    public Animator animator;

    public LayerMask layer;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isdead = false;
        isActive = false;
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        player_script = player.GetComponent<Player_Controller>();
        moveDir = -1;

    }

    public void die()
    {
        isdead = true;
        Destroy(gameObject, 1f);
    }

    private void move()
    {
        transform.localScale = new Vector3(moveDir*-1, 1, 1);
        rig.velocity = new Vector2(moveSpeed * moveDir, rig.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isActive)
        {
            animator.SetBool("isdead", isdead);
            move();
            check_for_wall();
            
        }
        
    }

    private void OnBecameVisible()
    {
        isActive = true;
    }
    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(player.transform.position.y < transform.position.y)
            {
                player_script.die();
                die();
            }
            else
            {
                die();
            }

        }
        
    }

    private void check_for_wall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * moveDir, .75f, layer);
        if (hit)
        {
            moveDir *= -1;
        }

    }




}
