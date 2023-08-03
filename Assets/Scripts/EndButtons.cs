using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButtons : MonoBehaviour
{
    public void StartAgain()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void GoInMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
