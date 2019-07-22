using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    public bool gameOver = true;
    private UIManager _UIManager;
    [SerializeField]
    public GameObject player;
    
    void Update()
    {

        if (gameOver == true)
        {
            if ((Input.GetKeyDown(KeyCode.Space)))
            {
                _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                gameOver = false;
                Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                _UIManager.HideTitleScreen();
                _UIManager.resetScore();
                
                
            }
           

        }
    }
}

