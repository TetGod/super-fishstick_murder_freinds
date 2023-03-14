using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller_Script : MonoBehaviour
{
    public int levelNum;
    // Start is called before the first frame update
    void Start()
    {
        levelNum = SceneManager.GetActiveScene().buildIndex;
        print(levelNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void next_level()
    {
        SceneManager.LoadScene(levelNum+1);
    }
}
