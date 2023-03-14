using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Enemy : MonoBehaviour
{
    public float fallSpeed;
    public Rigidbody2D rig;
    public GameObject player;
    public Player_Controller player_script;
    public Vector3 startPos;
    public bool canFall;




    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        player_script = player.GetComponent<Player_Controller>();
        canFall = true;
        startPos = transform.position;
    }

    private void Update()
    {
        if (!canFall)
        {
            resetPos();
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_script.die();
        }
      
        
    }
    private void resetPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos, (fallSpeed/2)* Time.deltaTime);
        if (transform.position == startPos)
        {
            canFall = true;
        }
    }


}
