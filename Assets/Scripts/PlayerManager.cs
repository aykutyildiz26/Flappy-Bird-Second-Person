using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver,startGamePanel;
    public GameObject gameOverPanel,startPanel;
    void Start()
    {
        startGamePanel = true;
        gameOver = false;
        //Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        if (startGamePanel)
        {
            Time.timeScale = 0;
            startPanel.SetActive(true);
        }
        if(startGamePanel && Input.touchCount > 0)
        {
            //Debug.Log("Started Already!");
            startGamePanel=false;
            Time.timeScale = 1;
            startPanel.SetActive(false);
        }

    }
}
