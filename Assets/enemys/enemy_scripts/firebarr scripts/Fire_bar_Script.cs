using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_bar_Script : MonoBehaviour
{
    public GameObject player;
    public Player_Controller player_script;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_script = player.GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_script.die();
        }
    }
}
