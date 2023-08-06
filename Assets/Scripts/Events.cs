using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Events : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshPro otherPipeScoreText;

    private void Update()
    {
        scoreText.text = (int)TileController.score+"";
        otherPipeScoreText.text = (int)FlappyThingControl.pipeScore + "";
    }   
    public void QuitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void LeftArrow()
    {
        PlayerControl.desiredLane--;
        if (PlayerControl.desiredLane == -1)
           PlayerControl.desiredLane = 0;
        //Debug.Log(desiredLane);
    }
    public void RightArrow()
    {
       PlayerControl.desiredLane++;
        if (PlayerControl.desiredLane == 3)
           PlayerControl.desiredLane = 2;
        //Debug.Log(desiredLane);
    }
    public void JumpTheBird()
    {
        FlappyThingControl.rb.velocity = Vector3.up * FlappyThingControl.power;
    }
    public void RestartGame()
    {
        PlayerManager.gameOver = false;
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit(); //Temporary code...
    }
}
