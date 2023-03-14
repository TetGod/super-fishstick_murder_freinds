using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Enemy_Script : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sr;
    public bool isdead;

    public Move_Target target;

    public Vector3 startPos;
    public Vector3 targetPos;
    public Vector3 playerPos;

    public bool movingToTarget;

    public float moveSpeed;
    public bool seePlayer;
    public int facing;

    public LayerMask playerlayer;

   





    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        target = GetComponentInChildren<Move_Target>();
        startPos = transform.position;
        targetPos = target.transform.position;
        movingToTarget = true;
        seePlayer = false;
        facing = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
        move();
        lookForPlayer();
  
    }

    public void lookForPlayer()
    {
        RaycastHit2D seeL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .5f), new Vector2(-1, -1), 6f,playerlayer);
        RaycastHit2D seeR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .5f), new Vector2(1, -1), 6f,playerlayer);
        
        if (seeL)
        {
            if (seeL.collider.CompareTag("Player"))
            {
                seePlayer = true;
                playerPos = seeL.collider.transform.position;
            }
            

        }
        if (seeR)
        {
            if (seeR.collider.CompareTag("Player"))
            {
                seePlayer = true;
                playerPos = seeR.collider.transform.position;
            }


        }
    }

    public void move()
    {
        if (movingToTarget && !seePlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            if (transform.position == targetPos)
            {
                sr.flipX = true;
                facing *= -1;
                movingToTarget = false;
            }
        }
        else if (!movingToTarget && !seePlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            if (transform.position == startPos)
            {
                facing *= -1;
                sr.flipX=false;
                movingToTarget = true;
            }
        }
        if (seePlayer)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, playerPos, ((moveSpeed+2) * Time.deltaTime));
            if (transform.position.y <= playerPos.y+.5f)
            {
               
               seePlayer=false;
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (collision.transform.position.y > transform.position.y)
            {

                die();
            }
            else
            {
                collision.gameObject.GetComponent<Player_Controller>().die();
                
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public void die()
    {
        animator.SetBool("die", true);
        Destroy(gameObject,1f);
    }

}
