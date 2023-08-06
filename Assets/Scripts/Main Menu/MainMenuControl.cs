using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControl : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level");
    }
}
