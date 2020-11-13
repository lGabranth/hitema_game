using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject parent;

    public void PlayGame()
    {
        parent.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
